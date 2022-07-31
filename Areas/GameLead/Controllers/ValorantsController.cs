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
    public class ValorantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ValorantsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: GameLead/Valorants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Valorants.ToListAsync());
        }

        // GET: GameLead/Valorants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorant = await _context.Valorants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valorant == null)
            {
                return NotFound();
            }

            return View(valorant);
        }

        // GET: GameLead/Valorants/Create
        public IActionResult Create()
        {

            Valorant model = new Valorant
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

        // POST: GameLead/Valorants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] Valorant valorant)
        {
            if (ModelState.IsValid)
            {
                if (valorant.ImageFile != null)
                {
                    //Save image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(valorant.ImageFile.FileName);
                    string extension = Path.GetExtension(valorant.ImageFile.FileName);
                    valorant.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/teams/teams/valorant/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await valorant.ImageFile.CopyToAsync(fileStream);
                    }
                }

                _context.Add(valorant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(valorant);
        }

        // GET: GameLead/Valorants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorant = await _context.Valorants.FindAsync(id);
            if (valorant == null)
            {
                return NotFound();
            }

            valorant.TeamNumberItems = new List<SelectListItem>
            {

                new SelectListItem {Value = "1", Text = "Team 1"},

                new SelectListItem {Value = "2", Text = "Team 2"},

                new SelectListItem {Value = "3", Text = "Team 3"},

                new SelectListItem {Value = "4", Text = "Team 4"},
            };

            return View(valorant);
        }

        // POST: GameLead/Valorants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile,ImageName")] Valorant valorant)
        {
            if (id != valorant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (valorant.ImageName != null && valorant.ImageFile != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/teams/valorant/", valorant.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(valorant.ImageFile.FileName);
                        string extension = Path.GetExtension(valorant.ImageFile.FileName);
                        valorant.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/valorant/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await valorant.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else if (valorant.ImageName == null && valorant.ImageFile != null)
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(valorant.ImageFile.FileName);
                        string extension = Path.GetExtension(valorant.ImageFile.FileName);
                        valorant.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/valorant/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await valorant.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(valorant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValorantExists(valorant.Id))
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
            return View(valorant);
        }

        // GET: GameLead/Valorants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorant = await _context.Valorants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valorant == null)
            {
                return NotFound();
            }

            return View(valorant);
        }

        // POST: GameLead/Valorants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valorant = await _context.Valorants.FindAsync(id);

            if (valorant.ImageName != null)
            {
                //delete image from wwwroot/image
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/teams/valorant/", valorant.ImageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }


            _context.Valorants.Remove(valorant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValorantExists(int id)
        {
            return _context.Valorants.Any(e => e.Id == id);
        }
    }
}
