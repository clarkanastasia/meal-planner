using Microsoft.EntityFrameworkCore;
using MealPlanner.Models.Data;

namespace MealPlanner;

public class MealPlannerContext: DbContext
{
  public DbSet<Item> Items {get; set;} = null!;
  public DbSet<Ingredient> Ingredients {get; set;} = null!;
  public DbSet<Recipe> Recipes {get; set;} = null!;

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=dinner; Username=dinner; Password=dinner;");
  }
}