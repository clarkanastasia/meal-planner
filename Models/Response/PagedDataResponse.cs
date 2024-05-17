namespace MealPlanner.Models.Response;

public class PagedDataResponse
{
  public required List<RecipeResponse> Data {get; set;}

  public required int CurrentPage {get; set;}

  public required int ItemsPerPage {get; set;}

  public required int TotalPages {get; set;}
}