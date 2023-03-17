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
        IActionResult respuesta = null;
        if(id <= 0)
        {
          respuesta = BadRequest();
        }
        Pizza p = BD.BuscarPizzaPorId(id);

        if(p == null)
        {
          respuesta = NotFound();
        }
        else{
        respuesta =  Ok(p);
        }
        return respuesta;
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
      IActionResult respuesta = null;
      int intRowsAffected;
      if(pizza.Id != id)
      {
          respuesta = BadRequest();
      }

       else{
        intRowsAffected = BD.ActualizarPizza(id, pizza);
        if (intRowsAffected > 0){
            respuesta = Ok(pizza);
        }
        else{
          respuesta = NotFound();
        }
        
       }
      return respuesta;
    }

  [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
      int intRowsAffected;
      IActionResult respuesta = null;
       if(id <= 0)
       {
          respuesta = BadRequest();
       }
       else{
          intRowsAffected =  BD.BorrarPizzas(id);
          if(intRowsAffected > 0){
              respuesta = Ok();
          }
          else{
            respuesta = NotFound();
          }
       }
       
      
       return respuesta;
    }


}
