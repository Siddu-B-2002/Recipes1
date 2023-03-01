namespace FreshRecipes.Models
{
  public class Recipe
  {
    public Guid id { get; set; }
    public string recipe { get; set; }
    public int amount { get; set; }
    public string restaurant { get; set; }
    public string city { get; set; }
  }

}
