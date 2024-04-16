using System;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	internal class Recipe
	{
		private string recipeName;
		private int ingredientCount;
		private int stepCount;
		private Ingredient[] ingredients;
		private string[] recipeSteps;
		private double scale = 1;

		public Recipe()
		{
		}

		public string RecipeName
		{
			get
			{
				return recipeName;
			}
			set
			{
				recipeName = value;
			}
		}

		public int IngredientCount
		{
			get
			{
				return ingredientCount;
			}
			set
			{
				ingredientCount = value;
			}
		}

		public int StepCount
		{
			get
			{
				return stepCount;
			}
			set
			{
				stepCount = value;
			}
		}

		public void setIngredients(Ingredient[] ingredients)
		{
			this.ingredients = ingredients;
		}

		public Ingredient getIngredient(int ingredientIndex)
		{
			return ingredients[ingredientIndex];
		}

		public void setRecipeSteps(string[] recipeSteps)
		{
			this.recipeSteps = recipeSteps;
		}

		public string getRecipeStep(int stepIndex)
		{
			return recipeSteps[stepIndex];
		}

		public void setScale(double scale)
		{
			this.scale = scale;
		}

		public double getScale()
		{
			return scale;
		}

		public void resetScale()
		{
			this.scale = 1;
		}

		public void Reset()
		{
			recipeName = "";
			ingredientCount = 0;
			stepCount = 0;
			ingredients = null;
			recipeSteps = null;
			scale = 1;
		}

		public string displayRecipe()
		{
			string fullRecipe = "";
			fullRecipe += ("\n----" + this.recipeName + " Recipe----");
			fullRecipe += ("\n\n----Ingredients----");
			for (int step = 0; step < ingredientCount; step++)
			{
				fullRecipe += ("\n" + this.ingredients[step].Name + " " + (this.ingredients[step].Amount) * scale + this.ingredients[step].Measurment);
			}
			fullRecipe += ("\n\n----Recipe Steps----");
			for (int step = 0; step < stepCount; step++)
			{
				fullRecipe += ("\n" + this.recipeSteps[step]);
			}
			return fullRecipe;
		}
	}
}
