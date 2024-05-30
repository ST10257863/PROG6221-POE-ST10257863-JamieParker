using System.Text;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	// Define a delegate that represents a method taking an int (calories)
	public delegate void CalorieCountHandler(int calories);

	internal class Recipe
	{
		// Define an event based on the delegate
		public event CalorieCountHandler CalorieCountExceeded;

		// Declare the ingredients and recipe steps arrays, and the scale factor
		private Ingredient[] ingredients;
		private string[] recipeSteps;
		private double scale = 1;
		private int totalCalories;

		public double TotalCalories
		{
			get
			{
				return totalCalories;
			}
			set
			{
				totalCalories = (int)value;
				// If the total calories exceed 300, raise the event
				if (totalCalories * scale > 300)
				{
					CalorieCountExceeded?.Invoke(totalCalories);
				}
			}
		}

		// Default constructor
		public Recipe()
		{
		}

		// Properties for recipe name, ingredient count, and step count
		public string RecipeName
		{
			get; set;
		}
		public int IngredientCount
		{
			get; set;
		}
		public int StepCount
		{
			get; set;
		}

		// Method to set the ingredients array
		public void setIngredients(Ingredient[] ingredients)
		{
			this.ingredients = ingredients;
		}

		// Method to get a specific ingredient
		public Ingredient getIngredient(int ingredientIndex)
		{
			return ingredients[ingredientIndex];
		}

		// Method to set the recipe steps array
		public void setRecipeSteps(string[] recipeSteps)
		{
			this.recipeSteps = recipeSteps;
		}

		// Method to get a specific recipe step
		public string getRecipeStep(int stepIndex)
		{
			return recipeSteps[stepIndex];
		}

		// Method to set the scale factor
		public void setScale(double scale)
		{
			this.scale = scale;
		}

		// Method to get the scale factor
		public double getScale()
		{
			return scale;
		}

		// Method to reset the scale factor to 1
		public void ResetScale()
		{
			this.scale = 1;
		}

		// Method to reset all properties and fields to their default values
		public void Reset()
		{
			RecipeName = "";
			IngredientCount = 0;
			StepCount = 0;
			ingredients = null;
			recipeSteps = null;
			ResetScale();
			TotalCalories = 0;
		}

		// Method to display the full recipe
		public string displayRecipe()
		{
			StringBuilder fullRecipe = new StringBuilder();
			TotalCalories = 0;
			fullRecipe.AppendLine("\n----" + this.RecipeName + " Recipe----");
			fullRecipe.AppendLine("\n----Ingredients----");
			for (int step = 0; step < IngredientCount; step++)
			{
				fullRecipe.AppendLine(this.ingredients[step].Name + " " + (this.ingredients[step].Amount * scale) + " " + this.ingredients[step].Measurment);
				TotalCalories += (this.ingredients[step].Calories * scale); // Assuming Ingredient class has a Calories property
			}
			fullRecipe.AppendLine("\n\n----Recipe Steps----");
			for (int step = 0; step < StepCount; step++)
			{
				fullRecipe.AppendLine(this.recipeSteps[step]);
			}
			fullRecipe.AppendLine("\n\nTotal Calories: " + TotalCalories);
			return fullRecipe.ToString();
		}
	}
}
