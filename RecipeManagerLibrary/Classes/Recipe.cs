using System;
using System.Collections.Generic;
using System.Text;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	// Define a delegate that represents a method taking an int (calories)
	public delegate void CalorieCountHandler(int calories);

	public class Recipe
	{
		public event CalorieCountHandler CalorieCountExceeded;
		private double scale = 1;
		private int totalCalories;

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
		public List<Ingredient> Ingredients
		{
			get; set;
		}
		public List<string> RecipeSteps
		{
			get; set;
		}

		public double TotalCalories
		{
			get
			{
				CalculateTotalCalories();
				return totalCalories;
			}
			private set
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

		public void AddIngredient(Ingredient ingredient)
		{
			Ingredients.Add(ingredient);
			CalculateTotalCalories();
		}

		public void AddRecipeStep(string step)
		{
			RecipeSteps.Add(step);
		}

		public Ingredient GetIngredient(int ingredientIndex)
		{
			return Ingredients[ingredientIndex];
		}

		public string GetRecipeStep(int stepIndex)
		{
			return RecipeSteps[stepIndex];
		}

		public void SetScale(double scale)
		{
			this.scale = scale;
		}

		public double GetScale()
		{
			return scale;
		}

		public void ResetScale()
		{
			this.scale = 1;
		}

		private void CalculateTotalCalories()
		{
			totalCalories = 0;
			foreach (var item in Ingredients)
			{
				totalCalories += Convert.ToInt32(item.Calories);
			}
			TotalCalories = totalCalories * scale; // Trigger the calorie check
		}

		public void Reset()
		{
			RecipeName = "";
			Ingredients.Clear();
			RecipeSteps.Clear();
			ResetScale();
			TotalCalories = 0;
		}

		public string DisplayRecipe()
		{
			StringBuilder fullRecipe = new StringBuilder();
			TotalCalories = 0;
			fullRecipe.AppendLine("\n----" + RecipeName + " Recipe----");
			fullRecipe.AppendLine("\n----Ingredients----");
			for (int step = 0; step < Ingredients.Count; step++)
			{
				fullRecipe.AppendLine(Ingredients[step].Name + " " + (Ingredients[step].Amount * scale) + " " + Ingredients[step].Measurement);
				TotalCalories += (Ingredients[step].Calories * scale);
			}
			fullRecipe.AppendLine("\n\n----Recipe Steps----");
			for (int step = 0; step < RecipeSteps.Count; step++)
			{
				fullRecipe.AppendLine(RecipeSteps[step]);
			}
			fullRecipe.AppendLine("\n\nTotal Calories: " + TotalCalories);
			return fullRecipe.ToString();
		}
	}
}
