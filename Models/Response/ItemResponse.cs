using MealPlanner.Enums;

namespace MealPlanner.Models.Response;

public class ItemResponse
{
  public required int Id {get; set;}
  public required string Name {get; set;}

  public required Category Category {get; set;} 
}
