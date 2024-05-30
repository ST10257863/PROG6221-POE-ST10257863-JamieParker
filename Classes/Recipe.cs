using System.Collections.Generic;
using System.Text;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	// Define a delegate that represents a method taking an int (calories)
	public delegate void CalorieCountHandler(int calories);

	public class Recipe
	{
		public event CalorieCountHandler CalorieCountExceeded;

		public List<Ingredient> Ingredients
		{
			get; set;
		}
		public List<string> RecipeSteps
		{
			get; set;
		}
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
				if (totalCalories * scale > 300)
				{
					CalorieCountExceeded?.Invoke(totalCalories);
				}
			}
		}

		public Recipe()
		{
			Ingredients = new List<Ingredient>();
			RecipeSteps = new List<string>();
		}

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

		public void AddIngredient(Ingredient ingredient)
		{
			Ingredients.Add(ingredient);
		}

		public void AddRecipeStep(string step)
		{
			RecipeSteps.Add(step);
		}

		public Ingredient getIngredient(int ingredientIndex)
		{
			return Ingredients[ingredientIndex];
		}

		public string getRecipeStep(int stepIndex)
		{
			return RecipeSteps[stepIndex];
		}

		public void setScale(double scale)
		{
			this.scale = scale;
		}

		public double getScale()
		{
			return scale;
		}

		public void ResetScale()
		{
			this.scale = 1;
		}

		public void Reset()
		{
			RecipeName = "";
			IngredientCount = 0;
			StepCount = 0;
			Ingredients = null;
			RecipeSteps = null;
			ResetScale();
			TotalCalories = 0;
		}

		public string displayRecipe()
		{
			StringBuilder fullRecipe = new StringBuilder();
			TotalCalories = 0;
			fullRecipe.AppendLine("\n----" + this.RecipeName + " Recipe----");
			fullRecipe.AppendLine("\n----Ingredients----");
			for (int step = 0; step < IngredientCount; step++)
			{
				fullRecipe.AppendLine(this.Ingredients[step].Name + " " + (this.Ingredients[step].Amount * scale) + " " + this.Ingredients[step].Measurement);
				TotalCalories += (this.Ingredients[step].Calories * scale);
			}
			fullRecipe.AppendLine("\n\n----Recipe Steps----");
			for (int step = 0; step < StepCount; step++)
			{
				fullRecipe.AppendLine(this.RecipeSteps[step]);
			}
			fullRecipe.AppendLine("\n\nTotal Calories: " + TotalCalories);
			return fullRecipe.ToString();
		}
	}
}
