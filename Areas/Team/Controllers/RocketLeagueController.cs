using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WattEsportsCore.Data;
using WattEsportsCore.Models.ViewModels;

namespace WattEsportsCore.Areas.Team.Controllers
{
    [Area("Team")]
    public class RocketLeagueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RocketLeagueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team/RocketLeagueTeam
        public IActionResult RocketLeagueTeam()
        {
            // Creating a new RocketLeagueTeamsViewModel
            RocketLeagueTeamsViewModel model = new RocketLeagueTeamsViewModel
            {
                // Adding to the view model a list of all products
                TeamOneList = (from RocketLeague in this._context.RocketLeagues
                               select RocketLeague).ToList().Where(v => v.SelectedTeamNumber.Equals("1")),
                TeamTwoList = (from RocketLeague in this._context.RocketLeagues
                               select RocketLeague).ToList().Where(v => v.SelectedTeamNumber.Equals("2")),
                TeamThreeList = (from RocketLeague in this._context.RocketLeagues
                                 select RocketLeague).ToList().Where(v => v.SelectedTeamNumber.Equals("3")),
                TeamFourList = (from RocketLeague in this._context.RocketLeagues
                                select RocketLeague).ToList().Where(v => v.SelectedTeamNumber.Equals("4")),
                TeamFiveList = (from RocketLeague in this._context.RocketLeagues
                                select RocketLeague).ToList().Where(v => v.SelectedTeamNumber.Equals("5")),
            };

            return View(model);

        }
    }
}
