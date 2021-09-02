using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WattEsportsCore.Data;
using WattEsportsCore.Models;
using WattEsportsCore.Models.ViewModels;

namespace WattEsportsCore.Areas.Team.Controllers
{
    [Area("Team")]
    public class LeagueOfLegendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueOfLegendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team/CSGOTeam
        public IActionResult LoLTeam()
        {
            // Creating a new RocketLeagueTeamsViewModel
            LeagueOfLegendsTeamsViewModel model = new LeagueOfLegendsTeamsViewModel
            {
                // Adding to the view model a list of all products
                TeamOneList = (from LeagueOfLegends in this._context.LeagueOfLegends
                               select LeagueOfLegends).ToList().Where(v => v.SelectedTeamNumber.Equals("1")),
                TeamTwoList = (from LeagueOfLegends in this._context.LeagueOfLegends
                               select LeagueOfLegends).ToList().Where(v => v.SelectedTeamNumber.Equals("2")),
                TeamThreeList = (from LeagueOfLegends in this._context.LeagueOfLegends
                                 select LeagueOfLegends).ToList().Where(v => v.SelectedTeamNumber.Equals("3")),
                TeamFourList = (from LeagueOfLegends in this._context.LeagueOfLegends
                                select LeagueOfLegends).ToList().Where(v => v.SelectedTeamNumber.Equals("4")),
                TeamFiveList = (from LeagueOfLegends in this._context.LeagueOfLegends
                                select LeagueOfLegends).ToList().Where(v => v.SelectedTeamNumber.Equals("5")),
            };

            return View(model);

        }
    }
}
