using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WattEsportsCore.Data;
using WattEsportsCore.Models;

namespace WattEsportsCore.Areas.GameLead.Controllers
{
    
    [Area("GameLead")]
    [Authorize(Roles = "ServerManager, CommitteeCoordinator, CommitteeEsports," +
        " CommitteePresident, CommitteeSecretary, CommitteeTreasurer, R6Lead, ValorantLead," +
        " MinecraftLead, CSGOLead, FightingLead, LeagueOfLegendsLead, DOTALead, RocketLeagueLead ")]
    public class RainbowSixesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public RainbowSixesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: GameLead/RainbowSixes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RainbowSixs.ToListAsync());
        }

        // GET: GameLead/RainbowSixes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rainbowSix = await _context.RainbowSixs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rainbowSix == null)
            {
                return NotFound();
            }

            return View(rainbowSix);
        }

        // GET: GameLead/RainbowSixes/Create
        public IActionResult Create()
        {
            RainbowSix model = new RainbowSix
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

        // POST: GameLead/RainbowSixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] RainbowSix rainbowSix)
        {
            if (ModelState.IsValid)
            {
                if (rainbowSix.ImageFile != null)
                {
                    //Save image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(rainbowSix.ImageFile.FileName);
                    string extension = Path.GetExtension(rainbowSix.ImageFile.FileName);
                    rainbowSix.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/teams/rainbowsix/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await rainbowSix.ImageFile.CopyToAsync(fileStream);
                    }
                }

                _context.Add(rainbowSix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rainbowSix);
        }

        // GET: GameLead/RainbowSixes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rainbowSix = await _context.RainbowSixs.FindAsync(id);
            if (rainbowSix == null)
            {
                return NotFound();
            }
            
            rainbowSix.TeamNumberItems = new List<SelectListItem>
            {

                new SelectListItem {Value = "1", Text = "Team 1"},

                new SelectListItem {Value = "2", Text = "Team 2"},

                new SelectListItem {Value = "3", Text = "Team 3"},

                new SelectListItem {Value = "4", Text = "Team 4"},
            };

            return View(rainbowSix);
        }

        // POST: GameLead/RainbowSixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] RainbowSix rainbowSix)
        {
            if (id != rainbowSix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (rainbowSix.ImageName != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/rainbowsix/", rainbowSix.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(rainbowSix.ImageFile.FileName);
                        string extension = Path.GetExtension(rainbowSix.ImageFile.FileName);
                        rainbowSix.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/rainbowsix/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rainbowSix.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else if (rainbowSix.ImageFile != null) // We are not saving the default image again woo
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(rainbowSix.ImageFile.FileName);
                        string extension = Path.GetExtension(rainbowSix.ImageFile.FileName);
                        rainbowSix.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/rainbowsix/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rainbowSix.ImageFile.CopyToAsync(fileStream);
                        }
                    }


                    _context.Update(rainbowSix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RainbowSixExists(rainbowSix.Id))
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
            return View(rainbowSix);
        }

        // GET: GameLead/RainbowSixes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rainbowSix = await _context.RainbowSixs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rainbowSix == null)
            {
                return NotFound();
            }

            return View(rainbowSix);
        }

        // POST: GameLead/RainbowSixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.RainbowSixs.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/teams/rainbowsix/", player.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            var rainbowSix = await _context.RainbowSixs.FindAsync(id);
            _context.RainbowSixs.Remove(rainbowSix);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RainbowSixExists(int id)
        {
            return _context.RainbowSixs.Any(e => e.Id == id);
        }
    }
}
