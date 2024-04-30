using MealPlanner.Enums;

namespace MealPlanner.Models.Request;

public class CreateItemRequest
{
  public required string Name {get; set;}
  public required Category Category {get; set;}
}