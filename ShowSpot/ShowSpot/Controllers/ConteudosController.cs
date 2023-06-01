using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShowSpot.Data;
using ShowSpot.Models;

namespace ShowSpot.Controllers
{
    public class ConteudosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ConteudosController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<Conteudos> ConteudosList()
        {
            var conteudos = _context.Conteudos.ToList();

            return conteudos;
        }

        public IActionResult VistaFilmes()
        {
            var result = _context.Conteudos
                .Where(c => c.Tipo == false)
                .OrderByDescending(c => c.Id)
                .Take(50)
                .Select(c => new
                {
                    c.Id,
                    c.Nome,
                    c.ImgUrl,
                    c.Sinopse,
                    c.Rating,
                    c.Tipo,
                    c.Runtime,
                    c.AnoLancamento,
                    c.LinkTrailer,
                    Tag = _context.ConteudoTags
                        .Where(ct => ct.ConteudoFK == c.Id)
                        .Join(_context.Tags, ct => ct.TagFK, t => t.Id, (ct, t) => t.Nome)
                        .FirstOrDefault()
                });

            var tags = _context.Tags.Select(t => t.Nome);

            ViewBag.Filmes = JsonConvert.SerializeObject(result);
            ViewBag.Tags = JsonConvert.SerializeObject(tags);

            return View();
        }

        public IActionResult VistaSeries()
        {
            var conteudos = ConteudosList();
            return View(conteudos);
        }

        // GET: Conteudos
        public async Task<IActionResult> Index()
        {
              return _context.Conteudos != null ? 
                          View(await _context.Conteudos.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Conteudos'  is null.");
        }

        // GET: Conteudos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conteudos == null)
            {
                return NotFound();
            }

            var conteudos = await _context.Conteudos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conteudos == null)
            {
                return NotFound();
            }

            return View(conteudos);
        }

        // GET: Conteudos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conteudos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,ImgUrl,Sinopse,Rating,Tipo,Runtime")] Conteudos conteudos)
        {
            string runtimeRegex = "";
            string errorMessage = "";

            if (conteudos.Tipo)
            {
                // x episodes, y seasons
                runtimeRegex = @"^\d+ episódios, \d+ temporadas$";
                errorMessage = "Runtime deve de ser 'x episódios, y temporadas'";
            }
            else
            {
                runtimeRegex = @"^[1-4]h[0-5]?\d$";
                errorMessage = "Runtime deve de ser algo como xhy, com x entre 1 e 4, e y entre 0 e 59";
            }

            if (string.IsNullOrEmpty(conteudos.Runtime) || !Regex.IsMatch(conteudos.Runtime, runtimeRegex))
            {
                ModelState.AddModelError("Runtime", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _context.Add(conteudos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(conteudos);
        }

        // GET: Conteudos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conteudos == null)
            {
                return NotFound();
            }

            var conteudos = await _context.Conteudos.FindAsync(id);
            if (conteudos == null)
            {
                return NotFound();
            }
            return View(conteudos);
        }

        // POST: Conteudos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,ImgUrl,Sinopse,Rating,Tipo,Runtime")] Conteudos conteudos)
        {
            if (id != conteudos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conteudos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConteudosExists(conteudos.Id))
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
            return View(conteudos);
        }

        // GET: Conteudos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Conteudos == null)
            {
                return NotFound();
            }

            var conteudos = await _context.Conteudos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conteudos == null)
            {
                return NotFound();
            }

            return View(conteudos);
        }

        // POST: Conteudos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conteudos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Conteudos'  is null.");
            }
            var conteudos = await _context.Conteudos.FindAsync(id);
            if (conteudos != null)
            {
                _context.Conteudos.Remove(conteudos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConteudosExists(int id)
        {
          return (_context.Conteudos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
