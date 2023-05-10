using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShowSpot.Models;
using ShowSpot.Data;

namespace ShowSpot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET
        [HttpGet]
        public JsonResult Get()
        {
            // Retorna uma lista de 50 os filmes
            var result = _context.Conteudos.Where(c => c.Tipo == false).OrderByDescending(c => c.Id).Take(50);
            
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
    }
}
