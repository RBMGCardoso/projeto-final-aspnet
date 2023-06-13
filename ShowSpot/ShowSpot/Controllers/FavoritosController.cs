using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShowSpot.Data;
using ShowSpot.Models;

namespace ShowSpot.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Favoritos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Favoritos.Include(f => f.Conteudo).Include(f => f.User);
            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(await applicationDbContext.ToListAsync());
            return Forbid();
            
        }

        // GET: Favoritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }

            var favoritos = await _context.Favoritos
                .Include(f => f.Conteudo)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoritos == null)
            {
                return NotFound();
            }
            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(favoritos); 
            return Forbid();
        }

        // GET: Favoritos/Create
        public IActionResult Create()
        {
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome");
            ViewData["UserFK"] = new SelectList(_context.Users, "Id", "UserName");
            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(); 
            return Forbid();
        }

        // POST: Favoritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserFK,ConteudosFK")] Favoritos favoritos)
        {
                _context.Add(favoritos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Favoritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }

            var favoritos = await _context.Favoritos.FindAsync(id);
            if (favoritos == null)
            {
                return NotFound();
            }
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome", favoritos.ConteudosFK);
            ViewData["UserFK"] = new SelectList(_context.Users, "Id", "UserName", favoritos.UserFK);
            
            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(favoritos); 
            return Forbid();
        }

        // POST: Favoritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserFK,ConteudosFK")] Favoritos favoritos)
        {
            if (id != favoritos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoritos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoritosExists(favoritos.Id))
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
            ViewData["ConteudosFK"] = new SelectList(_context.Conteudos, "Id", "Nome", favoritos.ConteudosFK);
            ViewData["UserFK"] = new SelectList(_context.Users, "Id", "UserName", favoritos.UserFK);
            return View(favoritos);
        }

        // GET: Favoritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }

            var favoritos = await _context.Favoritos
                .Include(f => f.Conteudo)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoritos == null)
            {
                return NotFound();
            }

            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(favoritos); 
            return Forbid();
        }

        // POST: Favoritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Favoritos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Favoritos'  is null.");
            }
            var favoritos = await _context.Favoritos.FindAsync(id);
            if (favoritos != null)
            {
                _context.Favoritos.Remove(favoritos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoritosExists(int id)
        {
          return (_context.Favoritos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
