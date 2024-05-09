namespace MealPlanner.Models.Request;

public class CreateIngredientRequest
{
    public required string ItemName {get; set;}
    public required float Quantity {get; set;}
    public required string Unit {get; set;}
}