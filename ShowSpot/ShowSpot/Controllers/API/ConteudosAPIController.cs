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
            var result = _context.Conteudos.Where(c => c.Tipo == false).OrderByDescending(c => c.Id).Take(50);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //GET para as series
        [HttpGet("series")]
        public JsonResult GetSeries()
        {
            // Retorna uma lista de 50 os filmes
            var result = _context.Conteudos.Where(c => c.Tipo == true).OrderByDescending(c => c.Id).Take(50);

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

        //GET para ir buscar as tags dado o id do conteúdo
        [HttpGet("tags")]
        public JsonResult GetTags(int id)
        {
            var result = _context.ConteudoTags.Where(c => c.ConteudoFK == id).OrderByDescending(c => c.ConteudoFK).Take(50);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }
    }
}
