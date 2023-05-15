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

namespace ShowSpot.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConteudosAPIController(ApplicationDbContext context)
        {
            _context = context;
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
        
        // GET ID
        [HttpGet("{id}")]
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
                    Tag = _context.ConteudoTags
                        .Where(ct => ct.ConteudoFK == c.Id)
                        .Join(_context.Tags, ct => ct.TagFK, t => t.Id, (ct, t) => t.Nome)
                        .FirstOrDefault()
                });;

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
    }
}
