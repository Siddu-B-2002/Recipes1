using FreshRecipes.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshRecipes.Data
{
  public class FreshRecipesDBContext: DbContext
  {
    public FreshRecipesDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }

  }
}
