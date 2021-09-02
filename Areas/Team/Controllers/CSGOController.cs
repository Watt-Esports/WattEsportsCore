﻿using System;
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
    public class CSGOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CSGOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team/CSGOTeam
        public IActionResult CSGOTeam()
        {
            // Creating a new ValorantTeamsViewModel
            CSGOTeamsViewModel model = new CSGOTeamsViewModel
            {
                // Adding to the view model a list of all products
                TeamOneList = (from Counterstrike in this._context.Counterstrike
                               select Counterstrike).ToList().Where(v => v.SelectedTeamNumber.Equals("1")),
                TeamTwoList = (from Counterstrike in this._context.Counterstrike
                               select Counterstrike).ToList().Where(v => v.SelectedTeamNumber.Equals("2")),
                TeamThreeList = (from Counterstrike in this._context.Counterstrike
                                 select Counterstrike).ToList().Where(v => v.SelectedTeamNumber.Equals("3")),
                TeamFourList = (from Counterstrike in this._context.Counterstrike
                                select Counterstrike).ToList().Where(v => v.SelectedTeamNumber.Equals("4")),
                TeamFiveList = (from Counterstrike in this._context.Counterstrike
                                select Counterstrike).ToList().Where(v => v.SelectedTeamNumber.Equals("5")),
            };

            return View(model);

        }
    }
}
