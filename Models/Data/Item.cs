using MealPlanner.Enums;

namespace MealPlanner.Models.Data;

public class Item 
{
  public int Id {get; set;}
  public required string Name {get; set;}
  public required Category Category {get; set;}
}