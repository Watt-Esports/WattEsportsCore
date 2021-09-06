using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WattEsportsCore.Data;
using WattEsportsCore.Models.ViewModels;

namespace WattEsportsCore.Areas.Team.Controllers
{
    [Area("Team")]
    public class ValorantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValorantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team/ValorantTeam
        public IActionResult ValorantTeam()
        {
            // Creating a new ValorantTeamsViewModel
            ValorantTeamsViewModel model = new ValorantTeamsViewModel
            {
                // Adding to the view model a list of all products
                TeamOneList = (from Valorants in this._context.Valorants
                                 select Valorants).ToList().Where(v => v.SelectedTeamNumber.Equals("1")),
                TeamTwoList = (from Valorants in this._context.Valorants
                            select Valorants).ToList().Where(v => v.SelectedTeamNumber.Equals("2")),
                TeamThreeList = (from Valorants in this._context.Valorants
                               select Valorants).ToList().Where(v => v.SelectedTeamNumber.Equals("3")),
                TeamFourList = (from Valorants in this._context.Valorants
                                 select Valorants).ToList().Where(v => v.SelectedTeamNumber.Equals("4")),
                TeamFiveList = (from Valorants in this._context.Valorants
                                select Valorants).ToList().Where(v => v.SelectedTeamNumber.Equals("5")),
            };

            return View(model);

        }

        private bool ValorantExists(int id)
        {
            return _context.Valorants.Any(e => e.Id == id);
        }
    }
}
