using FreshRecipes.Data;
using FreshRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*namespace FreshRecipes.Controllers
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
    //private readonly ApiDbContext dbConetext;//Used when using EF
    private readonly FreshRecipesDBContext _freshRecipesDBContext;

    public RecipesController(IConfiguration configuration)
    {
      _connection = new NpgsqlConnection(configuration.GetConnectionString("FreshRecipesConnectionString"));
      
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //Using Dapper
    public async Task<IActionResult> GetAllRecipes()
    {
        List<Recipe> orders = new List<Recipe>();
        orders = (await _connection.QueryAsync<Recipe>("SELECT \"id\", \"recipe\", \"amount\", \"restaurant\", \"city\"\r\n\tFROM public.\"Recipes\";", new { })).ToList();
        return Ok(orders);
    }


    /*[HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //Using Dapper
    public async Task<IActionResult> AddEmployees([FromBody] Recipe recipe)
    {
        var result=(await _connection.ExecuteAsync("INSERT INTO public.\"Recipes\"(\r\n\t \"recipe\", \"amount\", \"restaurant\", \"city\")\r\n\tVALUES (@recipe, @amount, @restaurant, @city)",recipe));
        return Ok(recipe);
    }*/
    [HttpPost]
    public async Task<IActionResult> AddRecipe([FromBody] Recipe recipeReq)
    {
      recipeReq.id = Guid.NewGuid();
      await _freshRecipesDBContext.Recipes.AddAsync(recipeReq);
      await _freshRecipesDBContext.SaveChangesAsync();
      return Ok(recipeReq);
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //Using dapper
    //public async Task<IActionResult> GetEmployee([FromRoute] int id)
    //{
    //    var result = (await _connection.QueryAsync<Employee>("SELECT * FROM public.\"Employees\" WHERE \"Id\"=@id", new { id }).ConfigureAwait(false)).FirstOrDefault();
    //    if (result == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(result);
    //}


    //[HttpPut]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //Using Dapper
    //public async Task<IActionResult> EditEmployee([FromBody] Employee employee)
    //{
    //    var result = (await _connection.ExecuteAsync("UPDATE public.\"Employees\"\r\n\tSET \"Name\"=@Name, \"Email\"=@Email, \"Phone\"=@Phone, \"Salary\"=@Salary, \"Department\"=@Department\r\n\tWHERE \"Id\"=@Id;", employee));
    //    return Ok(result);
    //}
    //Using EF

    //[HttpDelete("{id:int}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //Using Dapper
    //public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    //{
    //    var employee = (await _connection.QueryAsync<Employee>("SELECT * FROM public.\"Employees\" WHERE \"Id\"=@id", new {id}).ConfigureAwait(false)).FirstOrDefault();
    //    if (employee == null)
    //    {
    //        return NotFound();
    //    }
    //    var result = (await _connection.ExecuteAsync("DELETE FROM public.\"Employees\"\r\n\tWHERE \"Id\"=@Id;", new { id }));
    //    return Ok(result);
    //}

  }
}
