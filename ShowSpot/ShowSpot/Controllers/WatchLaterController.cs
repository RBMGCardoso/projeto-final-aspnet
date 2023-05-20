using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShowSpot.Data;
using ShowSpot.Models;

namespace ShowSpot.Controllers
{
    public class WatchLaterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchLaterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WatchLater
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WatchLaters.Include(w => w.Conteudo).Include(w => w.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WatchLater/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WatchLaters == null)
            {
                return NotFound();
            }

            var watchLaters = await _context.WatchLaters
                .Include(w => w.Conteudo)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchLaters == null)
            {
                return NotFound();
            }

            return View(watchLaters);
        }

        // GET: WatchLater/Create
        public IActionResult Create()
        {
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome");
            ViewData["UtilizadorFK"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: WatchLater/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UtilizadorFK,ConteudosFK")] WatchLaters watchLaters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watchLaters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome", watchLaters.ConteudosFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Users, "Id", "UserName", watchLaters.UtilizadorFK);
            return View(watchLaters);
        }

        // GET: WatchLater/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WatchLaters == null)
            {
                return NotFound();
            }

            var watchLaters = await _context.WatchLaters.FindAsync(id);
            if (watchLaters == null)
            {
                return NotFound();
            }
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome", watchLaters.ConteudosFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Users, "Id", "UserName", watchLaters.UtilizadorFK);
            return View(watchLaters);
        }

        // POST: WatchLater/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UtilizadorFK,ConteudosFK")] WatchLaters watchLaters)
        {
            if (id != watchLaters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchLaters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchLatersExists(watchLaters.Id))
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
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome", watchLaters.ConteudosFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Users, "Id", "UserName", watchLaters.UtilizadorFK);
            return View(watchLaters);
        }

        // GET: WatchLater/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WatchLaters == null)
            {
                return NotFound();
            }

            var watchLaters = await _context.WatchLaters
                .Include(w => w.Conteudo)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchLaters == null)
            {
                return NotFound();
            }

            return View(watchLaters);
        }

        // POST: WatchLater/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WatchLaters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WatchLaters'  is null.");
            }
            var watchLaters = await _context.WatchLaters.FindAsync(id);
            if (watchLaters != null)
            {
                _context.WatchLaters.Remove(watchLaters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchLatersExists(int id)
        {
          return (_context.WatchLaters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
