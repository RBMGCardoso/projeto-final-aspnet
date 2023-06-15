using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders.Composite;
using Newtonsoft.Json;
using NuGet.Protocol;
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

    public IActionResult Index()
    {
        var user = _context.Users.Where(u => u.UserName == User.FindFirstValue(ClaimTypes.Name)).Take(1).ToList();

        if (user == null)
        {
            return new JsonResult(NotFound());
        }
            
        // Retorna as tags(nome) de todos os favoritos do utilizador
        var userFavs =
            _context.Favoritos.Where(f => f.UserFK == user[0].Id)
                .Select(f => new
                {
                    f.ConteudosFK
                })
                .Join(_context.ConteudoTags, c => c.ConteudosFK, t => t.ConteudoFK, (c, t) => t.TagFK);

        var recomendados = new List<object>();
        var excluded = _context.Favoritos.Select(f => f.ConteudosFK).AsEnumerable();

        var recDetalhes = new List<Object>();

        try
        {
            foreach (var favTag in userFavs)
            {
                var query = _context.ConteudoTags.Where(t => t.TagFK == favTag).Select(c => c.ConteudoFK).AsEnumerable().OrderDescending().Except(excluded).Take(1);
                
                recomendados.Add(query);
            }

            foreach (var rec in recomendados)
            {
                var recInt = rec.ToJson().Replace("[","").Replace("]","");
                var query = _context.Conteudos.Where(t => t.Id == int.Parse(recInt)).FirstOrDefault();
                recDetalhes.Add(query);
            }
            
            ViewBag.Recomendados = JsonConvert.SerializeObject(recDetalhes);
        }
        catch (Exception e)
        {
            
        }

        ViewBag.conteudos = JsonConvert.SerializeObject(_context.Conteudos.Where(c => c.Tipo == false && c.AnoLancamento == "2023").Take(3));
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
