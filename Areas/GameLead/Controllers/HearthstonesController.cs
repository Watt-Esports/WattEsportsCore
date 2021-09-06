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
    public class HearthstonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HearthstonesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: GameLead/Hearthstones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hearthstones.ToListAsync());
        }

        // GET: GameLead/Hearthstones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hearthstone = await _context.Hearthstones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hearthstone == null)
            {
                return NotFound();
            }

            return View(hearthstone);
        }

        // GET: GameLead/Hearthstones/Create
        public IActionResult Create()
        {
            Hearthstone model = new Hearthstone
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

        // POST: GameLead/Hearthstones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] Hearthstone hearthstone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (hearthstone.ImageFile != null)
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(hearthstone.ImageFile.FileName);
                        string extension = Path.GetExtension(hearthstone.ImageFile.FileName);
                        hearthstone.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/hearthstone/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await hearthstone.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                _context.Add(hearthstone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hearthstone);
        }

        // GET: GameLead/Hearthstones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hearthstone = await _context.Hearthstones.FindAsync(id);
            if (hearthstone == null)
            {
                return NotFound();
            }

            hearthstone.TeamNumberItems = new List<SelectListItem>
            {

                new SelectListItem {Value = "1", Text = "Team 1"},

                new SelectListItem {Value = "2", Text = "Team 2"},

                new SelectListItem {Value = "3", Text = "Team 3"},

                new SelectListItem {Value = "4", Text = "Team 4"},
            };

            return View(hearthstone);
        }

        // POST: GameLead/Hearthstones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IGN,Rank,InGameRole,SelectedTeamNumber,ImageFile")] Hearthstone hearthstone)
        {
            if (id != hearthstone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hearthstone.ImageName != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/hearthstone/", hearthstone.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(hearthstone.ImageFile.FileName);
                        string extension = Path.GetExtension(hearthstone.ImageFile.FileName);
                        hearthstone.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/hearthstone/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await hearthstone.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else if (hearthstone.ImageFile != null) // We are not saving the default image again woo
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(hearthstone.ImageFile.FileName);
                        string extension = Path.GetExtension(hearthstone.ImageFile.FileName);
                        hearthstone.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/teams/hearthstone/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await hearthstone.ImageFile.CopyToAsync(fileStream);
                        }
                    }


                    _context.Update(hearthstone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HearthstoneExists(hearthstone.Id))
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
            return View(hearthstone);
        }

        // GET: GameLead/Hearthstones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hearthstone = await _context.Hearthstones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hearthstone == null)
            {
                return NotFound();
            }

            return View(hearthstone);
        }

        // POST: GameLead/Hearthstones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hearthstone = await _context.Hearthstones.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/teams/hearthstone/", hearthstone.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);


            _context.Hearthstones.Remove(hearthstone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HearthstoneExists(int id)
        {
            return _context.Hearthstones.Any(e => e.Id == id);
        }
    }
}
