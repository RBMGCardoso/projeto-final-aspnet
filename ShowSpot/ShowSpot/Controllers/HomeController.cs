using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders.Composite;
using ShowSpot.Data;
using ShowSpot.Models;

namespace ShowSpot.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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

    public List<dynamic> getFilmes()
    {
        var result = _context.Conteudos
            .Where(c => c.Tipo == false)
            .OrderByDescending(c => c.Id)
            .Take(50)
            .Select(c => new 
            {
                Id = c.Id,
                Nome = c.Nome,
                ImgUrl = c.ImgUrl,
                Sinopse = c.Sinopse,
                Rating = c.Rating,
                Tipo = c.Tipo,
                Runtime = c.Runtime,
                AnoLancamento = c.AnoLancamento,
                LinkTrailer = c.LinkTrailer,
                // Set Tag property separately
                Tag = _context.ConteudoTags
                    .Where(ct => ct.ConteudoFK == c.Id)
                    .Join(_context.Tags, ct => ct.TagFK, t => t.Id, (ct, t) => t.Nome)
                    .FirstOrDefault()
            })
            .ToList();

        return result;
    }

    public IActionResult Index()
    {
        var conteudos = ConteudosList();
        return View(conteudos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Filmes()
    {
        return View();
    }

    public IActionResult Series()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
