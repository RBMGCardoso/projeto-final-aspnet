using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using ShowSpot.Models;
using ShowSpot.Data;
using Newtonsoft.Json;
using System.Dynamic;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace ShowSpot.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ConteudosAPIController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        // GET ID
        [HttpGet("conteudo/{id}")]
        public JsonResult Get(int id)
        {
            var result = _context.Conteudos
                .Where(c => c.Id == id)
                .Take(50)
                .OrderByDescending(c => c.Id)
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
                });;

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // GET para os filmes
        [HttpGet("filmes")]
        public JsonResult GetFilmes()
        {
            // Retorna uma lista de 50 os filmes
            //esta query vai buscar cada conteï¿½do e adiciona no final a tag que corresponda a cada filme
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

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // GET Filme por tag
        [HttpGet("filmes/{tag}")]
        public JsonResult GetFilmeByTag(string tag)
        {
            var result = _context.ConteudoTags
                .Join(
                    _context.Tags,
                    ct => ct.TagFK,
                    t => t.Id,
                    (ct, t) => new { ConteudoTag = ct, Tag = t }
                )
                .Where(x => x.Tag.Nome == tag)
                .Select(x => x.ConteudoTag)
                .ToList()
                .Join(
                    _context.Conteudos,
                    ct => ct.ConteudoFK,
                    c => c.Id,
                    (ct, c) => c
                )
                .Where(c => c.Tipo == false)
                .ToList();
            
            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para as series
        [HttpGet("series")]
        public JsonResult GetSeries()
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

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }
        
        // GET Filme por tag
        [HttpGet("series/{tag}")]
        public JsonResult GetSerieByTag(string tag)
        {
            var result = _context.ConteudoTags
                .Join(
                    _context.Tags,
                    ct => ct.TagFK,
                    t => t.Id,
                    (ct, t) => new { ConteudoTag = ct, Tag = t }
                )
                .Where(x => x.Tag.Nome == tag)
                .Select(x => x.ConteudoTag)
                .ToList()
                .Join(
                    _context.Conteudos,
                    ct => ct.ConteudoFK,
                    c => c.Id,
                    (ct, c) => c
                )
                .Where(c => c.Tipo == true)
                .ToList();
            
            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para ir buscar os nomes das tags
        [HttpGet("nomeTags")]
        public JsonResult GetNomeTags()
        {
            // Retorna uma lista de 50 os filmes
            var result = _context.Tags.Take(50);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para ir buscar os nomes das tags
        [HttpGet("nomeTags/{id}")]
        public JsonResult GetNomeTags(int id)
        {
            // Retorna uma lista de 50 os filmes
            var result = _context.Tags.Take(50)
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    t.Nome,
                });

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para efetuar login, recebendo email e password
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LogInRequest login)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(login.Email);

            try
            {
                if (user != null)
                {
                    PasswordVerificationResult passwordResult = new PasswordHasher<IdentityUser>().VerifyHashedPassword(null, user.PasswordHash, login.Password);
                    if (passwordResult.Equals(PasswordVerificationResult.Success))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] LogInRequest model)
        {
            var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                // If registration fails, return the errors to the client
                return BadRequest(result.Errors);
            }
            
        }

        [Authorize]
        //GET apenas para testar o authorize
        [HttpGet("nomeTagsS")]
        public JsonResult GetNomeTagss()
        {
            // Retorna uma lista de 50 os filmes
            var result = _context.Tags.Take(50);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para receber que user está logado
        [HttpGet("getUser")]
        public JsonResult GetUser()
        {
            var result = User.FindFirstValue(ClaimTypes.Name);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para fazer logout 
        [HttpGet("logout")]
        public async Task logout()
        {
            await _signInManager.SignOutAsync();
        }

        // Retorna os recomendados para um user dado o id do user
        [HttpGet("recs/{nome}")]
        public async Task<JsonResult> GetRecomendados(string nome)
        {
            var user = _context.Users.Where(u => u.UserName == nome).Take(1).ToList();

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

            try
            {
                foreach (var favTag in userFavs)
                {
                    var query = _context.ConteudoTags.Where(t => t.TagFK == favTag).Select(c => c.ConteudoFK).AsEnumerable().OrderDescending().Except(excluded).Take(1);
                
                    recomendados.Add(query);
                }
            }
            catch (Exception e)
            {
                return new JsonResult(NotFound());
            }

            if (!recomendados.Any())
            {
                return new JsonResult(NotFound());
            }
            
            return new JsonResult(Ok(recomendados));
        }

        [HttpPost("addFavorito")]
        async public Task<IActionResult> AddFavorito()
        {

            if (Request.Method == "POST")
            {
                string idFilme = Request.Form["idFilme"];
                string username = Request.Form["username"];
                var user = await _userManager.FindByNameAsync(username);

                Favoritos fav = new Favoritos
                {
                    ConteudosFK = Convert.ToInt32(idFilme),
                    UserFK = user.Id
                };

                _context.Favoritos.Add(fav);
                _context.SaveChanges();

                return Ok(); // Devolve sucesso
            }

            return BadRequest(); // devolve uma má resposta
        }


        [HttpPut("passwordChange")]
        async public Task<IActionResult> PasswordChange()
        {
            if(Request.Method == "PUT")
            {
                string username = Request.Form["username"];
                string newPassword = Request.Form["password"];
                var user = await _userManager.FindByNameAsync(username);

                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                return Ok(passwordChangeResult);
            }
            return BadRequest();
        }
    }
}
