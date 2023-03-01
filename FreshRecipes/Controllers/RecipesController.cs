using FreshRecipes.Data;
using FreshRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshRecipes.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class RecipesController : Controller
  {
    private readonly FreshRecipesDBContext _freshRecipesDBContext;

    public RecipesController(FreshRecipesDBContext freshRecipesDBContext)
    {
      _freshRecipesDBContext = freshRecipesDBContext;
    }

    //GET
    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
      var orders = await _freshRecipesDBContext.Recipes.ToListAsync();
      return Ok(orders);
    }

    //POST
    [HttpPost]
    public async Task<IActionResult> AddRecipe([FromBody] Recipe recipeReq)
    {
      recipeReq.id = Guid.NewGuid();
      await _freshRecipesDBContext.Recipes.AddAsync(recipeReq);
      await _freshRecipesDBContext.SaveChangesAsync();
      return Ok(recipeReq);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetRecipe([FromRoute] Guid id)
    {
      var orders = await _freshRecipesDBContext.Recipes.FirstOrDefaultAsync(x => x.id==id);

      if (orders == null)
      {
        return NotFound();
      }
      return Ok(orders);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateRecipe([FromRoute] Guid id, Recipe updateRecipeReq)
    {
      var order = await _freshRecipesDBContext.Recipes.FindAsync(id);
      if(order == null)
      {
        return NotFound();        
      }

      order.recipe = updateRecipeReq.recipe;
      order.amount = updateRecipeReq.amount;
      order.restaurant = updateRecipeReq.restaurant;
      order.city = updateRecipeReq.city;

      await _freshRecipesDBContext.SaveChangesAsync();

      return Ok(order);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] Guid id)
    {
      var order = await _freshRecipesDBContext.Recipes.FindAsync(id);
      if(order == null)
      {
        return NotFound();
      }

      _freshRecipesDBContext.Recipes.Remove(order);
      await _freshRecipesDBContext.SaveChangesAsync();

      return Ok(order);
    }
  }
}
