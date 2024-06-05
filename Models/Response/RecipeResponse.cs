using MealPlanner.Enums;
using MealPlanner.Models.Data;
namespace MealPlanner.Models.Response;

public class RecipeResponse 
{
  public required int Id {get; set;}
  public required string Name {get; set;}
  public int Servings {get; set;}
  public int CookingTime {get; set;}
  public required List<IngredientResponse> RecipeIngredients {get; set;}
  public required List<string> Instructions {get; set;}
  public Diet DietType {get; set;}
  public string? Cuisine {get; set;}
  public string? Source {get; set;} 
}