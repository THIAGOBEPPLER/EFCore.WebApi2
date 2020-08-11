using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listheroi = _context.Herois
                            .Where(h =>EF.Functions.Like(h.Nome, $"%{nome}%"))
                            .OrderByDescending(h => h.Id)
                            .FirstOrDefault();
            //var listheroi = (from heroi in _context.Herois
            //                 where heroi.Nome.Contains(nome)
            //                 select heroi).ToList();
            return Ok(listheroi);
        }

        // GET api/values/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //var heroi = new Heroi { Nome = nameHero };

            var heroi = _context.Herois
                            .Where(h => h.Id == 3)
                            .FirstOrDefault();

            heroi.Nome = "Homem Aranha";

                //_context.Herois.Add(heroi);
            _context.SaveChanges();
            
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }






        // GET api/values/5
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                new Heroi { Nome = "Capitao America" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viuva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gaviao Arqueiro" },
                new Heroi { Nome = "Capita Marvel" }
                );
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                        .Where(x => x.Id == id)
                        .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();

        }
    }
}