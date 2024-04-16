using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	internal class Recipe
	{
		private string recipeName;
		private int ingredientCount;
		private int stepCount;
		private Ingredient[] ingredients;
		private string[] recipeSteps;

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

		public void setRecipe(string[] recipe)
		{
			this.recipeSteps = recipe;
		}
		public string[] getRecipe(int stepIndex)
		{
			return recipeSteps;
		}


		public String displayRecipe()
		{
			String fullRecipe = "";
			fullRecipe += ("\n----" + this.recipeName + " Recipe----");
			fullRecipe += ("\n\n----Ingredients----");
			for (int step = 0; step < ingredientCount; step++)
			{
				fullRecipe += ("\n" + this.ingredients[step].Name + " " + this.ingredients[step].Amount + this.ingredients[step].Measurment);
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
