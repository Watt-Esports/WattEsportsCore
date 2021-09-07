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

    public class LeagueOfLegendsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public LeagueOfLegendsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: GameLead/LeagueOfLegends
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeagueOfLegends.ToListAsync());
        }

        // GET: GameLead/LeagueOfLegends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueOfLegends = await _context.LeagueOfLegends
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueOfLegends == null)
            {
                return NotFound();
            }

            return View(leagueOfLegends);
        }

        // GET: GameLead/LeagueOfLegends/Create
        public IActionResult Create()
        {

            LeagueOfLegends model = new LeagueOfLegends
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

        // POST: GameLead/LeagueOfLegends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] LeagueOfLegends leagueOfLegends)
        {
            if (ModelState.IsValid)
            {

                if (leagueOfLegends.ImageFile != null)
                {
                    //Save image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(leagueOfLegends.ImageFile.FileName);
                    string extension = Path.GetExtension(leagueOfLegends.ImageFile.FileName);
                    leagueOfLegends.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/teams/teams/leagueoflegends/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await leagueOfLegends.ImageFile.CopyToAsync(fileStream);
                    }
                }

                _context.Add(leagueOfLegends);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leagueOfLegends);
        }

        // GET: GameLead/LeagueOfLegends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueOfLegends = await _context.LeagueOfLegends.FindAsync(id);
            if (leagueOfLegends == null)
            {
                return NotFound();
            }

            leagueOfLegends.TeamNumberItems = new List<SelectListItem>
            {

                new SelectListItem {Value = "1", Text = "Team 1"},

                new SelectListItem {Value = "2", Text = "Team 2"},

                new SelectListItem {Value = "3", Text = "Team 3"},

                new SelectListItem {Value = "4", Text = "Team 4"},
            };


            return View(leagueOfLegends);
        }

        // POST: GameLead/LeagueOfLegends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile, ImageName")] LeagueOfLegends leagueOfLegends)
        {
            if (id != leagueOfLegends.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (leagueOfLegends.ImageName != null && leagueOfLegends.ImageFile != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/teams/leagueOfLegends/", leagueOfLegends.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(leagueOfLegends.ImageFile.FileName);
                        string extension = Path.GetExtension(leagueOfLegends.ImageFile.FileName);
                        leagueOfLegends.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/leagueofLegends/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await leagueOfLegends.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else if (leagueOfLegends.ImageName == null && leagueOfLegends.ImageFile != null)
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(leagueOfLegends.ImageFile.FileName);
                        string extension = Path.GetExtension(leagueOfLegends.ImageFile.FileName);
                        leagueOfLegends.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/leagueofLegends/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await leagueOfLegends.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(leagueOfLegends);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueOfLegendsExists(leagueOfLegends.Id))
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
            return View(leagueOfLegends);
        }

        // GET: GameLead/LeagueOfLegends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueOfLegends = await _context.LeagueOfLegends
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueOfLegends == null)
            {
                return NotFound();
            }

            return View(leagueOfLegends);
        }

        // POST: GameLead/LeagueOfLegends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leagueOfLegends = await _context.LeagueOfLegends.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/teams/leagueoflegends/", leagueOfLegends.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.LeagueOfLegends.Remove(leagueOfLegends);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueOfLegendsExists(int id)
        {
            return _context.LeagueOfLegends.Any(e => e.Id == id);
        }
    }
}
