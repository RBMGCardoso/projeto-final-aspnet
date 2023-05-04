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
    public class RecomendadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecomendadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recomendados
        public async Task<IActionResult> Index()
        { 
            return View(await _context.Recomendados.ToListAsync());
        }

        // GET: Recomendados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recomendados == null)
            {
                return NotFound();
            }

            var recomendados = await _context.Recomendados
                .Include(r => r.Conteudo)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recomendados == null)
            {
                return NotFound();
            }

            return View(recomendados);
        }

        // GET: Recomendados/Create
        public IActionResult Create()
        {
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Id");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View();
        }

        // POST: Recomendados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UtilizadorFK,ConteudosFK")] Recomendados recomendados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recomendados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Id", recomendados.ConteudosFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", recomendados.UtilizadorFK);
            return View(recomendados);
        }

        // GET: Recomendados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recomendados == null)
            {
                return NotFound();
            }

            var recomendados = await _context.Recomendados.FindAsync(id);
            if (recomendados == null)
            {
                return NotFound();
            }
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Id", recomendados.ConteudosFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", recomendados.UtilizadorFK);
            return View(recomendados);
        }

        // POST: Recomendados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UtilizadorFK,ConteudosFK")] Recomendados recomendados)
        {
            if (id != recomendados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recomendados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecomendadosExists(recomendados.Id))
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
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Id", recomendados.ConteudosFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", recomendados.UtilizadorFK);
            return View(recomendados);
        }

        // GET: Recomendados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recomendados == null)
            {
                return NotFound();
            }

            var recomendados = await _context.Recomendados
                .Include(r => r.Conteudo)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recomendados == null)
            {
                return NotFound();
            }

            return View(recomendados);
        }

        // POST: Recomendados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recomendados == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recomendados'  is null.");
            }
            var recomendados = await _context.Recomendados.FindAsync(id);
            if (recomendados != null)
            {
                _context.Recomendados.Remove(recomendados);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecomendadosExists(int id)
        {
          return (_context.Recomendados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
