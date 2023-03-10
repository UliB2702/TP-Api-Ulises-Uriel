using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly ILogger<PizzasController> _logger;

    public PizzasController(ILogger<PizzasController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Pizza> lista = BD.BuscarPizzas();
        return Ok(lista);

    }

    [HttpGet("{id}")]
    public IActionResult GetbyId(int id)
    {
        if(id <= 0)
        {
          return BadRequest();
        }
        Pizza p = BD.BuscarPizzaPorId(id);

        if(p == null)
        {
          return NotFound();
        }

        return Ok(p);
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        BD.CrearPizzas(pizza);
        return Ok(pizza);
    }

  [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
      if(pizza.Id != id)
      {
          return BadRequest();
      }
      Pizza pizza2 = BD.BuscarPizzaPorId(id);
       if(pizza2 == null)
       {
          return NotFound();
       }
      BD.ActualizarPizza(id, pizza);
      return Ok();
    }

  [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
      
       if(id <= 0)
       {
          return BadRequest();
       }
      Pizza pizza = BD.BuscarPizzaPorId(id);
       if(pizza == null)
       {
          return NotFound();
       }
       BD.BorrarPizzas(id);
       return Ok();
    }


}
