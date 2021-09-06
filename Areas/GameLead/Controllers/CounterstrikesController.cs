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

    public class CounterstrikesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CounterstrikesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: GameLead/Counterstrikes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Counterstrike.ToListAsync());
        }

        // GET: GameLead/Counterstrikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterstrike = await _context.Counterstrike
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counterstrike == null)
            {
                return NotFound();
            }

            return View(counterstrike);
        }

        // GET: GameLead/Counterstrikes/Create
        public IActionResult Create()
        {
            Counterstrike model = new Counterstrike
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

        // POST: GameLead/Counterstrikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] Counterstrike counterstrike)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (counterstrike.ImageFile != null)
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(counterstrike.ImageFile.FileName);
                        string extension = Path.GetExtension(counterstrike.ImageFile.FileName);
                        counterstrike.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/counterstrike/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await counterstrike.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
   

                _context.Add(counterstrike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(counterstrike);
        }

        // GET: GameLead/Counterstrikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterstrike = await _context.Counterstrike.FindAsync(id);
            if (counterstrike == null)
            {
                return NotFound();
            }

            counterstrike.TeamNumberItems = new List<SelectListItem>
            {

                new SelectListItem {Value = "1", Text = "Team 1"},

                new SelectListItem {Value = "2", Text = "Team 2"},

                new SelectListItem {Value = "3", Text = "Team 3"},

                new SelectListItem {Value = "4", Text = "Team 4"},
            };


            return View(counterstrike);
        }

        // POST: GameLead/Counterstrikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] Counterstrike counterstrike)
        {
            if (id != counterstrike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (counterstrike.ImageName != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/counterstrike/", counterstrike.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(counterstrike.ImageFile.FileName);
                        string extension = Path.GetExtension(counterstrike.ImageFile.FileName);
                        counterstrike.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/counterstrike/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await counterstrike.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else if (counterstrike.ImageFile != null) // We are not saving the default image again woo
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(counterstrike.ImageFile.FileName);
                        string extension = Path.GetExtension(counterstrike.ImageFile.FileName);
                        counterstrike.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/counterstrike/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await counterstrike.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(counterstrike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounterstrikeExists(counterstrike.Id))
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
            return View(counterstrike);
        }

        // GET: GameLead/Counterstrikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterstrike = await _context.Counterstrike
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counterstrike == null)
            {
                return NotFound();
            }

            return View(counterstrike);
        }

        // POST: GameLead/Counterstrikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Counterstrike.FindAsync(id);


            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/teams/counterstrike/", player.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);


            var counterstrike = await _context.Counterstrike.FindAsync(id);
            _context.Counterstrike.Remove(counterstrike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounterstrikeExists(int id)
        {
            return _context.Counterstrike.Any(e => e.Id == id);
        }
    }
}
