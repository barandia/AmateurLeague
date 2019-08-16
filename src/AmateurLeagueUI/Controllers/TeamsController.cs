using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmateurLeague;
using Microsoft.AspNetCore.Http;

namespace AmateurLeagueUI.Controllers
{
    public class TeamsController : Controller
    {
        private LeagueManager LeagueManager;

        public TeamsController(LeagueManager leagueManager)
        {
            LeagueManager = leagueManager;
        }

        // GET: Teams
        public IActionResult Index()
        {
            return View(LeagueManager.GetAllTeams());
        }

        // GET: Teams/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = LeagueManager.GetTeamById(id.Value);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewBag.LeagueList = GetLeagueListItems();
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection data)
        {
            LeagueManager.CreateTeam(data["TeamName"], LeagueManager.GetLeague(int.Parse(data["LeagueId"])));
            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = LeagueManager.GetTeamById(id.Value);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection data)
        {
            if (id != int.Parse(data["TeamId"]))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var teamToUpdate = LeagueManager.GetTeamById(id);
                    teamToUpdate.TeamName = data["TeamName"];
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(int.Parse(data["TeamId"])))
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
            return View();
        }

        // GET: Teams/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = LeagueManager.GetTeamById(id.Value);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            LeagueManager.DeleteTeamById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return LeagueManager.IsTeamExist(id);
        }

        private List<SelectListItem> GetLeagueListItems()
        {
            var items = new List<SelectListItem>();

            foreach (var league in LeagueManager.GetAllLeagues().ToList())
            {
                items.Add(new SelectListItem { Text = league.LeagueName, Value = league.LeagueId.ToString() });
            }

            return items;
        }
    }
}
