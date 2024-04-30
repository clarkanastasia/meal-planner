using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlanner.Models.Data;

public class Ingredient
{
    public int Id {get; set;}
    public required float Quantity {get; set;}
    public required string Unit {get; set;}
    public int ItemId {get; set;}
    [ForeignKey("ItemId")]
    public Item Item {get; set;} = null!;
    public int RecipeId {get; set;}
    [ForeignKey("RecipeId")]
    public Recipe Recipe {get; set;} = null!;
}