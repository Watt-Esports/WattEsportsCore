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

namespace WattEsportsCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ServerManager, CommitteeCoordinator, CommitteeEsports, CommitteePresident, CommitteeSecretary, CommitteeTreasurer")]

    public class CommitteesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CommitteesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Committees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Committees.ToListAsync());
        }

        // GET: Admin/Committees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // GET: Admin/Committees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Committees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Role,ImageFile")] Committee committee)
        {
            if (ModelState.IsValid)
            {
                if (committee.ImageFile != null)
                {
                    //Save image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(committee.ImageFile.FileName);
                    string extension = Path.GetExtension(committee.ImageFile.FileName);
                    committee.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/committee/", fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    await committee.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(committee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(committee);
        }

        // GET: Admin/Committees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees.FindAsync(id);
            if (committee == null)
            {
                return NotFound();
            }
            return View(committee);
        }

        // POST: Admin/Committees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Role,ImageFile")] Committee committee)
        {
            if (id != committee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(committee.ImageName != null) // We delete it as it's not our default placeholder 
                    {
                        //delete image from wwwroot/image
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/images/committee/", committee.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);


                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(committee.ImageFile.FileName);
                        string extension = Path.GetExtension(committee.ImageFile.FileName);
                        committee.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/committee/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await committee.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else // We are using the default image for the person so we can just save the new one
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(committee.ImageFile.FileName);
                        string extension = Path.GetExtension(committee.ImageFile.FileName);
                        committee.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/committee/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await committee.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(committee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommitteeExists(committee.Id))
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
            return View(committee);
        }

        // GET: Admin/Committees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // POST: Admin/Committees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var committee = await _context.Committees.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/committee/", committee.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            //delete the record
            _context.Committees.Remove(committee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommitteeExists(int id)
        {
            return _context.Committees.Any(e => e.Id == id);
        }
    }
}
