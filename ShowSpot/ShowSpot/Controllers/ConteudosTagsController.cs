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
    public class ConteudosTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConteudosTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConteudosTags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConteudoTags.Include(c => c.Conteudo).Include(c => c.Tag);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConteudosTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConteudoTags == null)
            {
                return NotFound();
            }

            var conteudoTags = await _context.ConteudoTags
                .Include(c => c.Conteudo)
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conteudoTags == null)
            {
                return NotFound();
            }

            return View(conteudoTags);
        }

        // GET: ConteudosTags/Create
        public IActionResult Create()
        {
            ViewData["ConteudoFK"] = new SelectList(_context.Conteudos, "Id", "Id");
            ViewData["TagFK"] = new SelectList(_context.Tags, "Id", "Id");
            return View();
        }

        // POST: ConteudosTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConteudoFK,TagFK")] ConteudoTags conteudoTags)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conteudoTags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConteudoFK"] = new SelectList(_context.Conteudos, "Id", "Id", conteudoTags.ConteudoFK);
            ViewData["TagFK"] = new SelectList(_context.Tags, "Id", "Id", conteudoTags.TagFK);
            return View(conteudoTags);
        }

        // GET: ConteudosTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConteudoTags == null)
            {
                return NotFound();
            }

            var conteudoTags = await _context.ConteudoTags.FindAsync(id);
            if (conteudoTags == null)
            {
                return NotFound();
            }
            ViewData["ConteudoFK"] = new SelectList(_context.Conteudos, "Id", "Id", conteudoTags.ConteudoFK);
            ViewData["TagFK"] = new SelectList(_context.Tags, "Id", "Id", conteudoTags.TagFK);
            return View(conteudoTags);
        }

        // POST: ConteudosTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConteudoFK,TagFK")] ConteudoTags conteudoTags)
        {
            if (id != conteudoTags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conteudoTags);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConteudoTagsExists(conteudoTags.Id))
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
            ViewData["ConteudoFK"] = new SelectList(_context.Conteudos, "Id", "Id", conteudoTags.ConteudoFK);
            ViewData["TagFK"] = new SelectList(_context.Tags, "Id", "Id", conteudoTags.TagFK);
            return View(conteudoTags);
        }

        // GET: ConteudosTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConteudoTags == null)
            {
                return NotFound();
            }

            var conteudoTags = await _context.ConteudoTags
                .Include(c => c.Conteudo)
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conteudoTags == null)
            {
                return NotFound();
            }

            return View(conteudoTags);
        }

        // POST: ConteudosTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConteudoTags == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConteudoTags'  is null.");
            }
            var conteudoTags = await _context.ConteudoTags.FindAsync(id);
            if (conteudoTags != null)
            {
                _context.ConteudoTags.Remove(conteudoTags);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConteudoTagsExists(int id)
        {
          return (_context.ConteudoTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
