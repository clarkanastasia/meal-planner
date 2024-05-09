using MealPlanner.Models.Data;
using MealPlanner.Models.Request;
using MealPlanner.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MealPlanner.Controllers;

[ApiController]
[Route("/recipes")]
public class RecipeController(MealPlannerContext context): ControllerBase 
{
  private readonly MealPlannerContext _context = context;

  [HttpGet("all")]
  public IActionResult GetAllRecipes()
  {
    var allRecipes = _context.Recipes
    .AsNoTracking()
    .Include(recipes => recipes.RecipeIngredients)
      .ThenInclude(ingredients => ingredients.Item);

    var recipesList = new List<RecipeResponse>();

    foreach (var recipe in allRecipes)
    {
      var ingredientResponses = recipe.RecipeIngredients
      .Select(ingredient => new IngredientResponse
      {
        Id = ingredient.Id,
        ItemName = ingredient.Item.Name,
        Quantity = ingredient.Quantity,
        Unit = ingredient.Unit
      }).ToList(); 
      recipesList.Add(new RecipeResponse
      {
        Id = recipe.Id,
        Name = recipe.Name,
        Servings = recipe.Servings,
        CookingTime = recipe.CookingTime,
        RecipeIngredients = ingredientResponses,
        Instructions = recipe.Instructions,
        DietType = recipe.DietType,
        Cuisine = recipe.Cuisine,
        Source = recipe.Source
      });
    }

    var jsonRecipesList = JsonConvert.SerializeObject(recipesList);

    return Ok(jsonRecipesList);
  }

  [HttpGet("{id}")]
  public IActionResult GetRecipeById([FromRoute] int id)
  {
    var matchingRecipe = _context.Recipes
    .AsNoTracking()
    .Include(recipe => recipe.RecipeIngredients)
      .ThenInclude(ingredient => ingredient.Item)
    .SingleOrDefault(recipe => recipe.Id == id);

    if(matchingRecipe == null)
    {
      return NotFound();
    }

    var ingredientResponses = matchingRecipe.RecipeIngredients
      .Select(ingredient => new IngredientResponse
      {
        Id = ingredient.Id,
        ItemName = ingredient.Item.Name,
        Quantity = ingredient.Quantity,
        Unit = ingredient.Unit
      })
      .ToList();
    
    var matchingRecipeResponse = new RecipeResponse
    {
      Id = matchingRecipe.Id,
      Name = matchingRecipe.Name,
      Servings = matchingRecipe.Servings,
      CookingTime = matchingRecipe.CookingTime,
      RecipeIngredients = ingredientResponses,
      Instructions = matchingRecipe.Instructions,
      DietType = matchingRecipe.DietType,
      Cuisine = matchingRecipe.Cuisine,
      Source = matchingRecipe.Source
    };

    var jsonRecipeResponse = JsonConvert.SerializeObject(matchingRecipeResponse);
    return Ok(jsonRecipeResponse);
  }

  [HttpPost]
  public IActionResult AddRecipe([FromBody] CreateRecipeRequest createRecipeRequest)
  {
  
  var ingredientsList = new List<Ingredient>();

  foreach(CreateIngredientRequest ingredientRequest in createRecipeRequest.RecipeIngredients)
  {
    var mathchingItem =_context.Items.FirstOrDefault(item => item.Name == ingredientRequest.ItemName.ToLower());
    if(mathchingItem != null){
      ingredientsList.Add(new Ingredient
      {
        Quantity = ingredientRequest.Quantity,
        Unit = ingredientRequest.Unit,
        Item = mathchingItem
      });
    } else {
      return NotFound();
    }
  }
    _context.Recipes.Add(new Recipe 
    {
      Name = createRecipeRequest.Name,
      Servings = createRecipeRequest.Servings,
      CookingTime = createRecipeRequest.CookingTime,
      RecipeIngredients = ingredientsList,
      Instructions = createRecipeRequest.Instructions,
      DietType = createRecipeRequest.DietType,
      Cuisine = createRecipeRequest.Cuisine,
      Source = createRecipeRequest.Source
    });
    _context.SaveChanges();
    return Ok();
  }
[HttpDelete("{id}")]
  public IActionResult DeleteRecipe([FromRoute] int id)
  {
    var matchingRecipe = _context.Recipes.FirstOrDefault(item => item.Id == id);
    if (matchingRecipe == null)
    {
      return NotFound();
    }
    else 
    {
      _context.Recipes.Remove(matchingRecipe);
      _context.SaveChanges();
      return NoContent();
    }
  }
}