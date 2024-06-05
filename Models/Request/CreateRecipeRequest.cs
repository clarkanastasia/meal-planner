using MealPlanner.Enums;

namespace MealPlanner.Models.Request;

public class CreateRecipeRequest
{
  public required string Name {get; set;}
  public int Servings {get; set;}
  public int CookingTime {get; set;}
  public ICollection<CreateIngredientRequest> RecipeIngredients {get; set;} = [];
  public required List<string> Instructions {get; set;} = [];
  public Diet DietType {get; set;}
  public string? Cuisine {get; set;}
  public string? Source {get; set;} 
}