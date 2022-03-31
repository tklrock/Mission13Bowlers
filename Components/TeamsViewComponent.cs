using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bowlers.Models;

namespace Bowlers.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private IBowlerRepository repo { get; set; }

        public TeamsViewComponent(IBowlerRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["bowlerTeam"];
            var teams = repo.Bowlers
                .Select(x => x.Team.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }
    }
}
