using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WattEsportsCore.Data;
using WattEsportsCore.Models;

namespace WattEsportsCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PreviousCommitteesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreviousCommitteesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PreviousCommittees
        public async Task<IActionResult> Index()
        {
            return View(await _context.PreviousCommittees.ToListAsync());
        }

        // GET: Admin/PreviousCommittees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var previousCommittee = await _context.PreviousCommittees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (previousCommittee == null)
            {
                return NotFound();
            }

            return View(previousCommittee);
        }

        // GET: Admin/PreviousCommittees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PreviousCommittees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Role,Date")] PreviousCommittee previousCommittee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(previousCommittee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(previousCommittee);
        }

        // GET: Admin/PreviousCommittees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var previousCommittee = await _context.PreviousCommittees.FindAsync(id);
            if (previousCommittee == null)
            {
                return NotFound();
            }
            return View(previousCommittee);
        }

        // POST: Admin/PreviousCommittees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Role,Date")] PreviousCommittee previousCommittee)
        {
            if (id != previousCommittee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(previousCommittee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreviousCommitteeExists(previousCommittee.Id))
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
            return View(previousCommittee);
        }

        // GET: Admin/PreviousCommittees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var previousCommittee = await _context.PreviousCommittees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (previousCommittee == null)
            {
                return NotFound();
            }

            return View(previousCommittee);
        }

        // POST: Admin/PreviousCommittees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var previousCommittee = await _context.PreviousCommittees.FindAsync(id);
            _context.PreviousCommittees.Remove(previousCommittee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreviousCommitteeExists(int id)
        {
            return _context.PreviousCommittees.Any(e => e.Id == id);
        }
    }
}
