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

/*
 * Classe que representa a API utilizada na aplicação em React. Aqui estão todas as funções utilizadas na aplicação de React.
 */
namespace ShowSpot.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudosAPIController : ControllerBase
    {
        /*
         * Estes atributos permitem o manuseamento de dados na BD. Permitem o acesso ao context (base de dados) e a utilização 
         * das tabelas de utilizadores fornecidas pelo ASP.NET.
         */
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ConteudosAPIController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        // GET conteúdo dado o ID
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
            //procura na BD o utilizador com o email fornecido
            IdentityUser user = await _userManager.FindByEmailAsync(login.Email);

            try
            {
                if (user != null)
                {
                    /*
                    * se a password fornecida no pedido for igual à password que está na BD e essa password corresponder ao user que está a tentar logar,
                    * é validado o login.
                    */
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

        //POST para efetuar o registo de um utilizador no servidor. Utiliza a classe LogInRequest para processar o registo.
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

        //permite marcar um conteúdo como favorito para o utilizador logado no servidor.
        [HttpPost("addFavorito")]
        async public Task<JsonResult> AddFavorito()
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

                var query = _context.Favoritos.Where(u =>
                    u.UserFK == user.Id && u.ConteudosFK == Convert.ToInt32(idFilme)).FirstOrDefaultAsync();


                if (query.Result == null)
                {
                    _context.Favoritos.Add(fav);
                    _context.SaveChanges();
                    return new JsonResult(Ok("Add")); // Devolve sucesso // Devolve sucesso
                }
                else
                {
                    _context.Favoritos.Remove(_context.Favoritos.Single(u =>
                        u.UserFK == user.Id && u.ConteudosFK == Convert.ToInt32(idFilme)));
                    _context.SaveChanges();
                    return new JsonResult(Ok("Remove")); // Devolve sucesso // Devolve sucesso
                    
                }
            }

            return new JsonResult(BadRequest()); // devolve uma má resposta
        }
        
        //permite marcar um conteúdo para ver mais tarde para o utilizador logado no servidor.
        [HttpPost("addWatchLater")]
        async public Task<JsonResult> AddWatchLater()
        {
            if (Request.Method == "POST")
            {
                string idFilme = Request.Form["idFilme"];
                string username = Request.Form["username"];
                var user = await _userManager.FindByNameAsync(username);

                WatchLaters wL = new WatchLaters
                {
                    ConteudosFK = Convert.ToInt32(idFilme),
                    UtilizadorFK = user.Id
                };

                var query = _context.WatchLaters.Where(u =>
                    u.UtilizadorFK == user.Id && u.ConteudosFK == Convert.ToInt32(idFilme)).FirstOrDefaultAsync();


                if (query.Result == null)
                {
                    _context.WatchLaters.Add(wL);
                    _context.SaveChanges();
                    return new JsonResult(Ok("Add")); // Devolve sucesso
                }
                else
                {
                    _context.WatchLaters.Remove(_context.WatchLaters.Single(u =>
                        u.UtilizadorFK == user.Id && u.ConteudosFK == Convert.ToInt32(idFilme)));
                    _context.SaveChanges();
                    return new JsonResult(Ok("Remove")); // Devolve sucesso
                }

                

                
            }

            return new JsonResult(BadRequest()); // devolve uma má resposta
        }

        //função que permite a alteração da palavra-passe de um utilizador. 
        [HttpPut("passwordChange")]
        async public Task<IActionResult> PasswordChange(PasswordChangeRequest model)
        {
            //verifica a existencia do utilizador na DB
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);

            if(user == null)
            {
                return NotFound();
            }

            // Verifica se a password existente é igual à password fornecida
            var isSamePassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (isSamePassword)
            {
                return BadRequest("A nova password é igual à antiga");
            }

            //define um novo hash para a password do utilizador
            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

            user.PasswordHash = newPasswordHash;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                //sucesso
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        

        [HttpGet("getFavoritos/{nome}")]
        public JsonResult getFavorito(string nome)
        {
            var user = _context.Users.Where(u => u.UserName == nome).Take(1).ToList();

            if (user == null)
            {
                return new JsonResult(NotFound());
            }

            var userFavs =
                _context.Favoritos.Where(f => f.UserFK == user[0].Id)
                    .Select(f => f.ConteudosFK).ToList();
            
            
            return new JsonResult(Ok(userFavs));
        }
        
        [HttpGet("getWatchLaters/{nome}")]
        public JsonResult getWatchLaters(string nome)
        {
            var user = _context.Users.Where(u => u.UserName == nome).Take(1).ToList();

            if (user == null)
            {
                return new JsonResult(NotFound());
            }

            var userFavs =
                _context.WatchLaters.Where(f => f.UtilizadorFK == user[0].Id)
                    .Select(f => f.ConteudosFK).ToList();
            
            
            return new JsonResult(Ok(userFavs));
        }
    }
}
