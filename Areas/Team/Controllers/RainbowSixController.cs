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
    public class RainbowSixController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RainbowSixController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team/CSGOTeam
        public IActionResult RainbowSixTeam()
        {
            // Creating a new ValorantTeamsViewModel
            RainbowSixTeamsViewModel model = new RainbowSixTeamsViewModel
            {
                // Adding to the view model a list of all products
                TeamOneList = (from RainbowSixs in this._context.RainbowSixs
                               select RainbowSixs).ToList().Where(v => v.SelectedTeamNumber.Equals("1")),
                TeamTwoList = (from RainbowSixs in this._context.RainbowSixs
                               select RainbowSixs).ToList().Where(v => v.SelectedTeamNumber.Equals("2")),
                TeamThreeList = (from RainbowSixs in this._context.RainbowSixs
                                 select RainbowSixs).ToList().Where(v => v.SelectedTeamNumber.Equals("3")),
                TeamFourList = (from RainbowSixs in this._context.RainbowSixs
                                select RainbowSixs).ToList().Where(v => v.SelectedTeamNumber.Equals("4")),
                TeamFiveList = (from RainbowSixs in this._context.RainbowSixs
                                select RainbowSixs).ToList().Where(v => v.SelectedTeamNumber.Equals("5")),
            };

            return View(model);

        }
    }
}
