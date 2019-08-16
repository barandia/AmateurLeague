using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmateurLeague;
using AmateurLeague.Entity;
using System;

namespace AmateurLeagueUI.Controllers
{
    public class PlayersController : Controller
    {
        private LeagueManager LeagueManager;

        public PlayersController(LeagueManager leagueManager)
        {
            LeagueManager = leagueManager;
        }

        // GET: Players
        public IActionResult Index()
        {
            return View(LeagueManager.GetAllPlayers());
        }

        // GET: Players/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = LeagueManager.GetPlayer(id.Value);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewBag.DateNow = DateTime.Now;
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmailAddress,FirstName,LastName,Gender,DateOfBirth")] Player player)
        {
            if (ModelState.IsValid)
            {
                LeagueManager.CreatePlayer(player.EmailAddress, player.FirstName, player.LastName, player.Gender, player.DateOfBirth);
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = LeagueManager.GetPlayer(id.Value);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,EmailAddress,FirstName,LastName,Gender,DateOfBirth")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var playerToUpdate = LeagueManager.GetPlayer(id);
                    playerToUpdate.EmailAddress = player.EmailAddress;
                    playerToUpdate.FirstName = player.FirstName;
                    playerToUpdate.LastName = player.LastName;
                    playerToUpdate.Gender = player.Gender;
                    playerToUpdate.DateOfBirth = player.DateOfBirth;

                    LeagueManager.UpdatePlayer(playerToUpdate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
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
            return View(player);
        }

        // GET: Players/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = LeagueManager.GetPlayer(id.Value);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            LeagueManager.DeleteLeague(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return LeagueManager.IsPlayerExists(id);
        }
    }
}
