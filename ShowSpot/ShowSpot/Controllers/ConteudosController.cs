using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var result = _context.Conteudos
                .Where(c => c.Tipo == true)
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
            
            ViewBag.Series = JsonConvert.SerializeObject(result);
            ViewBag.Tags = JsonConvert.SerializeObject(tags);
            
            return View();
        }

        public IActionResult ViewBiblioteca()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            var user = _context.Users.Where(e => e.UserName == userEmail).FirstOrDefault();

            if (user == null)
            {
                return new JsonResult(NotFound());
            }

            var userFavs = _context.Favoritos
                .Where(f => f.UserFK == user.Id.ToString())
                .Join(_context.Conteudos, fav => fav.ConteudosFK, c => c.Id, (fav, c) => new
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
                })
                .OrderByDescending(c => c.Id)
                .Take(50)
                .ToList();

            ViewBag.Favoritos = JsonConvert.SerializeObject(userFavs);
            
            var userWL = _context.WatchLaters
                .Where(f => f.UtilizadorFK == user.Id.ToString())
                .Join(_context.Conteudos, fav => fav.ConteudosFK, c => c.Id, (fav, c) => new
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
                })
                .OrderByDescending(c => c.Id)
                .Take(50)
                .ToList();
            
            ViewBag.WatchLaters = JsonConvert.SerializeObject(userWL);

            return View();
        }

        // GET: Conteudos
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return _context.Conteudos != null ? 
                View(await _context.Conteudos.ToListAsync()) :
                Problem("Entity set 'ApplicationDbContext.Conteudos'  is null.");
            return Forbid();
              
        }

        // GET: Conteudos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conteudos == null)
            {
                return NotFound();
            }

            var conteudos = await _context.Conteudos
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
                }).FirstOrDefaultAsync(m => m.Id == id);
            if (conteudos == null)
            {
                return NotFound();
            }

            ViewBag.Conteudo = JsonConvert.SerializeObject(conteudos);

            return View();
        }

        // GET: Conteudos/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.Name);
            if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(); 
            return Forbid();
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
            
            var user = User.FindFirstValue(ClaimTypes.Name);
if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(conteudos); 
            return Forbid();
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

            var user = User.FindFirstValue(ClaimTypes.Name);
if(user != null) user = user.Split('@')[0];
            if(user == "admin") return View(conteudos); 
            return Forbid();
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
        
        
        [Route("/addFavorito/{idC}")]
        async public Task<IActionResult> addFavorito(int idC)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var user = _context.Users.Where(e => e.UserName == userEmail).FirstOrDefault();
            
            var query = _context.Favoritos.Where(u =>
                u.UserFK == user.Id && u.ConteudosFK == Convert.ToInt32(idC)).FirstOrDefaultAsync();
            
            Favoritos fav = new Favoritos
            {
                ConteudosFK = idC,
                UserFK = user.Id
            };
            
            if (query.Result == null)
            {
                _context.Favoritos.Add(fav);
                _context.SaveChanges();
                return RedirectToAction("Details", "Conteudos", new { id = idC });

            }
            else
            {
                _context.Favoritos.Remove(_context.Favoritos.Single(u =>
                    u.UserFK == user.Id && u.ConteudosFK == Convert.ToInt32(idC)));
                _context.SaveChanges();
                return RedirectToAction("Details", "Conteudos", new { id = idC });

            }
        }
        
        [Route("addWatchLater/{idC}")]
        async public Task<IActionResult> addWatchLater(int idC)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var user = _context.Users.Where(e => e.UserName == userEmail).FirstOrDefault();
            
            var query = _context.WatchLaters.Where(u =>
                u.UtilizadorFK == user.Id && u.ConteudosFK == Convert.ToInt32(idC)).FirstOrDefaultAsync();
            
            WatchLaters WL = new WatchLaters()
            {
                ConteudosFK = idC,
                UtilizadorFK = user.Id
            };
            
            if (query.Result == null)
            {
                _context.WatchLaters.Add(WL);
                _context.SaveChanges();
                return RedirectToAction("Details", "Conteudos", new { id = idC });

            }
            else
            {
                _context.WatchLaters.Remove(_context.WatchLaters.Single(u =>
                    u.UtilizadorFK == user.Id && u.ConteudosFK == Convert.ToInt32(idC)));
                _context.SaveChanges();
                return RedirectToAction("Details", "Conteudos", new { id = idC });

            }
        }
    }
}
