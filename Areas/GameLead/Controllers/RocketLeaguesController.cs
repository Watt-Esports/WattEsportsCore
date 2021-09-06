using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WattEsportsCore.Data;
using WattEsportsCore.Models;

namespace WattEsportsCore.Areas.GameLead.Controllers
{
    [Area("GameLead")]
    [Authorize(Roles = "ServerManager, CommitteeCoordinator, CommitteeEsports," +
        " CommitteePresident, CommitteeSecretary, CommitteeTreasurer, R6Lead, ValorantLead," +
        " MinecraftLead, CSGOLead, FightingLead, LeagueOfLegendsLead, DOTALead, RocketLeagueLead ")]
    public class RocketLeaguesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public RocketLeaguesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: GameLead/RocketLeagues
        public async Task<IActionResult> Index()
        {
            return View(await _context.RocketLeagues.ToListAsync());
        }

        // GET: GameLead/RocketLeagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rocketLeague = await _context.RocketLeagues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rocketLeague == null)
            {
                return NotFound();
            }

            return View(rocketLeague);
        }

        // GET: GameLead/RocketLeagues/Create
        public IActionResult Create()
        {
            RocketLeague model = new RocketLeague
            {
                TeamNumberItems = new List<SelectListItem>
                                    {

            new SelectListItem { Value = "1", Text = "Team 1" },

            new SelectListItem { Value = "2", Text = "Team 2" },

            new SelectListItem { Value = "3", Text = "Team 3" },

            new SelectListItem { Value = "4", Text = "Team 4" },
             }
            };

            return View(model);
        }

        // POST: GameLead/RocketLeagues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] RocketLeague rocketLeague)
        {
            if (ModelState.IsValid)
            {
                if (rocketLeague.ImageFile != null)
                {
                    //Save image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(rocketLeague.ImageFile.FileName);
                    string extension = Path.GetExtension(rocketLeague.ImageFile.FileName);
                    rocketLeague.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/teams/rocketleague/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await rocketLeague.ImageFile.CopyToAsync(fileStream);
                    }
                }


                _context.Add(rocketLeague);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rocketLeague);
        }

        // GET: GameLead/RocketLeagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rocketLeague = await _context.RocketLeagues.FindAsync(id);
            if (rocketLeague == null)
            {
                return NotFound();
            }

            rocketLeague.TeamNumberItems =new List<SelectListItem>
            {

                new SelectListItem { Value = "1", Text = "Team 1" },

                new SelectListItem { Value = "2", Text = "Team 2" },

                new SelectListItem { Value = "3", Text = "Team 3" },

                new SelectListItem { Value = "4", Text = "Team 4" },
            };

            return View(rocketLeague);
        }

        // POST: GameLead/RocketLeagues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] RocketLeague rocketLeague)
        {
            if (id != rocketLeague.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (rocketLeague.ImageName != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/rocketleague/", rocketLeague.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(rocketLeague.ImageFile.FileName);
                        string extension = Path.GetExtension(rocketLeague.ImageFile.FileName);
                        rocketLeague.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/rocketleague/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rocketLeague.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else if (rocketLeague.ImageFile != null) // We are not saving the default image again woo
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(rocketLeague.ImageFile.FileName);
                        string extension = Path.GetExtension(rocketLeague.ImageFile.FileName);
                        rocketLeague.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/rocketleague/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rocketLeague.ImageFile.CopyToAsync(fileStream);
                        }
                    }


                    _context.Update(rocketLeague);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RocketLeagueExists(rocketLeague.Id))
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
            return View(rocketLeague);
        }

        // GET: GameLead/RocketLeagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rocketLeague = await _context.RocketLeagues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rocketLeague == null)
            {
                return NotFound();
            }

            return View(rocketLeague);
        }

        // POST: GameLead/RocketLeagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rocketLeague = await _context.RocketLeagues.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/teams/rocketleague/", rocketLeague.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.RocketLeagues.Remove(rocketLeague);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RocketLeagueExists(int id)
        {
            return _context.RocketLeagues.Any(e => e.Id == id);
        }
    }
}
