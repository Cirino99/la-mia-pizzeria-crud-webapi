using la_mia_pizzeria_static.Data.Repository;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]", Order = 1)]

    [ApiController]
    public class PizzaController : ControllerBase
    {
        IPizzaRepository pizzaRepository;

        public PizzaController(IPizzaRepository _pizzaRepository)
        {
            pizzaRepository = _pizzaRepository;
        }
        public IActionResult Get()
        {
            List<Pizza> pizze = pizzaRepository.All();
            return Ok(pizze);
        }
        public IActionResult Search(string? name, int? categoryId)
        {
            List<Pizza> pizze = pizzaRepository.GetPizze(name, (int)categoryId);
            return Ok(pizze);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            Pizza pizza = pizzaRepository.GetById(id);
            return Ok(pizza);
        }
    }
}
