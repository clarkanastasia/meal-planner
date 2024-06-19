import { ChangeEvent, FormEventHandler, useState } from 'react';

interface Ingredient {
  itemName: string;
  quantity: number;
  unit: string;
}

const AddRecipe = () => {

  const [name, setName] = useState<string>('');
  const [servings, setServings] = useState<string>('');
  const [cookingTime, setCookingTime] = useState<string>('');
  const [instructions, setInstructions] = useState<string[]>(['']);
  const [ingredients, setIngredients] = useState<Ingredient[]>([{ itemName: '', quantity: 0, unit: '' }]);
  const [dietType, setDietType] = useState<string>('');
  const [source, setSource] = useState<string | null>(null);
  const [cuisine, setCuisine] = useState<string | null>(null);

  const handleInstructionChange = (index: number, event: ChangeEvent<HTMLInputElement>) => {
    const newInstructions = [...instructions];
    newInstructions[index] = event.target.value;
    setInstructions(newInstructions);
  };

  const handleAddInstruction = () => {
    setInstructions([...instructions, '']);
  };

  const handleRemoveInstruction = (index: number) => {
    const newInstructions = instructions.filter((_, i) => i !== index);
    setInstructions(newInstructions);
  };

  const handleIngredientChange = (index: number, field: keyof Ingredient, value: string | number ) => {
    const newIngredients = [...ingredients];
    newIngredients[index] = {
      ...newIngredients[index],
      [field]: value
    };
    setIngredients(newIngredients);
  };

  const handleAddIngredient = () => {
    setIngredients([...ingredients, { itemName: '', quantity: 0, unit: '' }]);
  };

  const handleRemoveIngredient = (index: number) => {
    const newIngredients = ingredients.filter((_, i) => i !== index);
    setIngredients(newIngredients);
  };

  const submitRecipe: FormEventHandler = (event) => {
    event.preventDefault()

    var newRecipe = {
      name: name, 
      servings: parseInt(servings, 10),
      cookingTime: parseInt(cookingTime,10),
      recipeIngredients: ingredients,
      instructions: instructions,
      dietType: parseInt(dietType, 10),
      cuisine : cuisine,
      source : source,
    }

    fetch("http://localhost:5046/recipes", {
      method: "post",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(newRecipe)
    })
    .then(response =>{
      if (response.ok){
          const form = event.target as HTMLFormElement
          form.reset()
      }
    })
  }

    return(
      <>
        <form onSubmit={submitRecipe} className="flex flex-col gap-y-4">
          <label>Name:
            <input 
            type="text" 
            name="name" 
            onChange={(event) => setName(event.target.value)}
            required />
          </label>
          <label>Number of servings:
            <input 
            type="number" 
            name="servings"
            onChange={(event) => setServings(event.target.value)} 
            required />
          </label>
          <label> Cooking time in minutes:
            <input 
            type="number" 
            name="cookingTime"
            onChange={(event) => setCookingTime(event.target.value)} 
            required/>
          </label>
          <h3>Add recipe ingredients:</h3>
          {ingredients.map((ingredient, index) => (
            <div key={index}>
            <label>
              Item name:
                <input
                type="text"
                value={ingredient.itemName}
                onChange={(event) => handleIngredientChange(index, 'itemName', event.target.value)}
                />
            </label>
            <label>
              Quantity:
                <input
                type="number"
                value={ingredient.quantity}
                onChange={(event) => handleIngredientChange(index, 'quantity', event.target.value)}
                />
          </label>
          <label>
            Unit:
            <input
              type="text"
              value={ingredient.unit}
              onChange={(event) => handleIngredientChange(index, 'unit', event.target.value)}
            />
          </label>
          <button type="button" onClick={() => handleRemoveIngredient(index)}>
            Remove
          </button>
        </div>
      ))}
      <button type="button" onClick={handleAddIngredient}>
        Add Ingredient
      </button>
      <h3>Add recipe instructions:</h3>
          {instructions.map((instruction, index) => (
            <div key={index}>
            <label>
              Step {index + 1}:
              <input
              type="text"
              value={instruction}
              onChange={(event) => handleInstructionChange(index, event)}
              required
            />
          </label>
          <button type="button" onClick={() => handleRemoveInstruction(index)}>
            Remove
          </button>
        </div>
      ))}
      <button type="button" onClick={handleAddInstruction}>
        Add Step
      </button>
          <label> Diet type:
            <select 
            defaultValue={0} 
            name="dietType" 
            onChange={(event) => setDietType(event.target.value)} 
            required>
                <option value={0}>Vegetarian</option>
                <option value={1}>Vegan</option>
                <option value={2}>Pescatarian</option>
                <option value={3}>Omnivore</option>
            </select>
          </label>
          <label>Cuisine:
            <input 
            type="text" 
            name="cuisine"
            onChange={(event) => setCuisine(event.target.value)} 
            />
          </label>
          <label>Source:
            <input 
            type="text" 
            name="source"
            onChange={(event) => setSource(event.target.value)} 
            />
          </label>
          <button type="submit">Add Recipe</button>
        </form>
      </>
    )
}

export default AddRecipe;