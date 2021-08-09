using System.Collections.Generic;
using DOTNET_Example.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using DOTNET_Example.Models;

namespace DOTNET_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        // private readonly PizzaService _pizzaService;
        
        // public PizzaController(PizzaService pizzaService)
        // {
        //     _pizzaService = pizzaService;
        // }

        // GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            PizzaService.GetAll();
        
        // GET all 2nd option
        // [HttpGet]
        // public async Task<ActionResult<List<CustomerOrder>>> Get() =>
        //     await _pizzaService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if(pizza == null)
                return NotFound();

            return pizza;
        }
        
        // POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {            
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }
        
        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if(existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);           

            return NoContent();
        }
        
        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }
        
        // // PATCH action
        // [HttpPatch("{id:int}")]
        // // public IActionResult Patch(int id, [FromBody]JsonPatchDocument pizza)
        // public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Pizza> pizza)
        // {
        //     // var entity = PizzaService.Pizzas.FirstOrDefault(p => p.Id == id);
        //     var p = PizzaService.Patch(id);
        //     
        //     if (p == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     pizza.ApplyTo(p); // Must have Microsoft.AspNetCore.Mvc.NewtonsoftJson installed
        //
        //     return Ok(p);
        // }   
        
        [HttpPatch]
        public IActionResult JsonPatchWithModelState(
            [FromBody] JsonPatchDocument<Pizza> patchDoc)
        {
            if (patchDoc != null)
            {
                var pizza = new Pizza();

                patchDoc.ApplyTo(pizza, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return new ObjectResult(pizza);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}