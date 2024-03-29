﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmateurLeague;
using AmateurLeague.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AmateurLeagueUI.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private LeagueManager LeagueManager;

        public LeaguesController(LeagueManager leagueManager)
        {
            LeagueManager = leagueManager;
        }

        // GET: Leagues
        public IActionResult Index()
        {
            return View(LeagueManager.GetAllLeagues());
        }

        // GET: Leagues/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = LeagueManager.GetLeague(id.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // GET: Leagues/Create
        public IActionResult Create()
        {
            ViewBag.SportList = GetSportListItems();
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection data)
        {
            LeagueManager.CreateLeague(data["LeagueName"], LeagueManager.GetSportById(data["SportId"]));
            return RedirectToAction(nameof(Index));
        }

        // GET: Leagues/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = LeagueManager.GetLeague(id.Value);
            if (league == null)
            {
                return NotFound();
            }
            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LeagueId,LeagueName")] League league)
        {
            if (id != league.LeagueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leagueToUpdate = LeagueManager.GetLeague(league.LeagueId);
                    leagueToUpdate.LeagueName = league.LeagueName;
                    LeagueManager.UpdateLeague(leagueToUpdate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueExists(league.LeagueId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(league);
        }

        // GET: Leagues/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = LeagueManager.GetLeague(id.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            LeagueManager.DeleteLeague(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueExists(int id)
        {
            return LeagueManager.IsLeagueExists(id);
        }

        private List<SelectListItem> GetSportListItems()
        {
            var items = new List<SelectListItem>();
            foreach (var sport in LeagueManager.GetAllSports().ToList())
            {
                items.Add(new SelectListItem { Text = sport.DisplayName, Value = sport.SportId});
            }

            return items;
        }
    }
}
