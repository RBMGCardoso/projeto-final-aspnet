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

        // GET para os filmes
        [HttpGet("filmes")]
        public JsonResult GetFilmes()
        {
            // Retorna uma lista de 50 os filmes
            //esta query vai buscar cada conte�do e adiciona no final a tag que corresponda a cada filme
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
                            Tag = _context.ConteudoTags
                            .Where(ct => ct.ConteudoFK == c.Id)
                            .Join(_context.Tags, ct => ct.TagFK, t => t.Id, (ct, t) => t.Nome)
                            .FirstOrDefault()
                        });

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }    
        
        // GET ID
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var result = _context.Conteudos.Find(id);

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
                            Tag = _context.ConteudoTags
                            .Where(ct => ct.ConteudoFK == c.Id)
                            .Join(_context.Tags, ct => ct.TagFK, t => t.Id, (ct, t) => t.Nome)
                            .FirstOrDefault()
                        });

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

        //GET para efetuar login, recebendo email e password
        [HttpGet("login/{email}/{password}")]
        public async Task<ActionResult> login(string email, string password)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);

            try
            {
                if(user != null)
                {
                    PasswordVerificationResult passWorks = new PasswordHasher<IdentityUser>().VerifyHashedPassword(null, user.PasswordHash, password);
                    if (passWorks.Equals(PasswordVerificationResult.Success))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch(Exception ex)
            {
                return NoContent();
            }


            return Ok(user);
        }

        [Authorize]
        //GET apenas para testar o authorize
        [HttpGet("nomeTagsS")]
        public JsonResult GetNomeTagsS()
        {
            // Retorna uma lista de 50 os filmes
            var result = _context.Tags.Take(50);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para receber que user est� logado
        [HttpGet("getUser")]
        public JsonResult GetUser()
        {
            // Retorna uma lista de 50 os filmes
            var result = User.FindFirstValue(ClaimTypes.Email);

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



    }
}
