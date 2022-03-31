using Bowlers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bowlers.Controllers
{
    public class HomeController : Controller
    {
        private IBowlerRepository _repo { get; set; }

        public HomeController(IBowlerRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(string bowlerTeam)
        {
            string header = "";
            if(bowlerTeam == null)
            {
                header = "All Bowlers";
            }
            else
            {
                header = bowlerTeam;
            }
            ViewBag.Header = header;
            var bowlers = _repo.Bowlers
                .Include(x => x.Team)
                .OrderBy(b => b.BowlerLastName)
                .Where(b => b.Team.TeamName == bowlerTeam || bowlerTeam == null)
                .ToList();
            return View(bowlers);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _repo.Bowlers
                .Select(x => x.Team)
                .Distinct()
                .OrderBy(x => x.TeamName)
                .ToList();
            return View("BowlerForm", new Bowler());
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler newBowler)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateBowler(newBowler);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = _repo.Bowlers
                .Select(x => x.Team)
                .Distinct()
                .OrderBy(x => x.TeamName)
                .ToList();
                return View(newBowler);
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerId)
        {
            ViewBag.Teams = _repo.Bowlers
                .Select(x => x.Team)
                .Distinct()
                .OrderBy(x => x.TeamName)
                .ToList();
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == bowlerId);
            return View("BowlerForm", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveBowler(b);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = _repo.Bowlers
                .Select(x => x.Team)
                .Distinct()
                .OrderBy(x => x.TeamName)
                .ToList();
                return View("BowlerForm", b);
            }
        }

        [HttpGet]
        public IActionResult Delete(int bowlerId)
        {
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == bowlerId);
            return View(bowler);
        }
        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            _repo.DeleteBowler(b);
            return RedirectToAction("Index");
        }
    }
}
