import { useEffect, useState } from "react";
import { recipeModel } from "./models/recipeModel";

const RecipeList = () => {
  
  const [recipeData, setRecipeData] = useState<recipeModel[] | null >(null);
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(true);

  const fetchRecipeData = () => {
    fetch('http://localhost:5046/recipes/all')
      .then(response => response.json())
      .then(data => {
        setRecipeData(data);
        console.log(data)
  })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  useEffect(() => {
    fetchRecipeData()
  }, [])

  return (
    <>
    {recipeData && (
      <div>
        {recipeData.map((recipe) => 
        <div key={recipe.Id}>
          <h3>{recipe.Name}</h3>
        </div>
        )}
      </div>
    )}
    {loading && <p>Loading...</p>}
    {error && <p>Unable to load data at this time</p>}
    </>
  )
}

export default RecipeList;
