using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WattEsportsCore.Data;
using WattEsportsCore.Models.ViewModels;

namespace WattEsportsCore.Areas.Team.Controllers
{
    [Area("Team")]
    public class HearthstoneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HearthstoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team/HearthstoneTeam
        public IActionResult HearthstoneTeam()
        {
            // Creating a new Hearthstone view model
            HearthstoneTeamsViewModel model = new HearthstoneTeamsViewModel
            {
                // Adding to the view model a list of all products
                TeamOneList = (from Hearthstones in this._context.Hearthstones
                    select Hearthstones).ToList().Where(v => v.SelectedTeamNumber.Equals("1")),
                TeamTwoList = (from Hearthstones in this._context.Hearthstones
                    select Hearthstones).ToList().Where(v => v.SelectedTeamNumber.Equals("2")),
                TeamThreeList = (from Hearthstones in this._context.Hearthstones
                    select Hearthstones).ToList().Where(v => v.SelectedTeamNumber.Equals("3")),
                TeamFourList = (from Hearthstones in this._context.Hearthstones
                    select Hearthstones).ToList().Where(v => v.SelectedTeamNumber.Equals("4")),
                TeamFiveList = (from Hearthstones in this._context.Hearthstones
                    select Hearthstones).ToList().Where(v => v.SelectedTeamNumber.Equals("5")),
            };

            return View(model);

        }
    }
}