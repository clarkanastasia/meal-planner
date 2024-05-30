export interface recipeModel {
  Id: number, 
  Name: string, 
  Servings: number,
  CookingTime: number,
  RecipeIngredients: {
    Id: number, 
    ItemName: string,
    Quantity: number,
    Unit: string,
  }[],
  Instructions: string,
  DietType: string,
  Cuisine? : string,
  Source? : string
}