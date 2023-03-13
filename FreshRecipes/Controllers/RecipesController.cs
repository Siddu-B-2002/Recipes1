using FreshRecipes.Data;
using FreshRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*
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
*/


using Dapper;
using Npgsql;
using System.Data;


namespace FreshRecipes.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RecipesController : Controller
  {
    private readonly IDbConnection _connection;//Used when using Dapper
   
    //private readonly FreshRecipesDBContext _freshRecipesDBContext;

    public RecipesController(IConfiguration configuration)
    {
      _connection = new NpgsqlConnection(configuration.GetConnectionString("FreshRecipesConnectionString"));
      
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRecipes()
    {
        List<Recipe> orders = new List<Recipe>();
        orders = (await _connection.QueryAsync<Recipe>("SELECT \"id\", \"recipe\", \"amount\", \"restaurant\", \"city\"\r\n\tFROM public.\"Recipes\";", new { })).ToList();
        return Ok(orders);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddRecipes([FromBody] Recipe recipe)
    {
        recipe.id = Guid.NewGuid();
        var result = (await _connection.ExecuteAsync("INSERT INTO public.\"Recipes\"(\r\n\t  \"id\",\"recipe\", \"amount\", \"restaurant\", \"city\")\r\n\tVALUES (@id,@recipe, @amount, @restaurant, @city);", recipe));
        return Ok(result);
    }


    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecipe([FromRoute] Guid id)
    {
        var result = (await _connection.QueryAsync<Recipe>("SELECT * FROM public.\"Recipes\" WHERE \"id\"=@id", new { id }).ConfigureAwait(false)).FirstOrDefault();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditRecipe([FromBody] Recipe recipe)
    {
        var result = (await _connection.ExecuteAsync("UPDATE public.\"Recipes\"\r\n\tSET \"recipe\"=@recipe, \"amount\"=@amount, \"restaurant\"=@restaurant, \"city\"=@city\r\n\tWHERE \"id\"=@id;", recipe));
        return Ok(result);
    }
    



    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe([FromRoute] Guid id)
    {
        var order = (await _connection.QueryAsync<Recipe>("SELECT * FROM public.\"Recipes\" WHERE \"id\"=@id", new {id}).ConfigureAwait(false)).FirstOrDefault();
        if (order == null)
        {
            return NotFound();
        }
        var result = (await _connection.ExecuteAsync("DELETE FROM public.\"Recipes\"\r\n\tWHERE \"id\"=@Id;", new { id }));
        return Ok(result);
    }
  }
}

