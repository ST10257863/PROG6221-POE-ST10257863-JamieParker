using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;
		private static Recipe recipes = new Recipe();

		static void Main(string[] args)
		{
			bool exitRequested = false;

			while (!exitRequested)
			{
				Console.WriteLine("Welcome to PROG6221 Recipe Keeper!");
				Console.WriteLine("Please enter the recipe name: ");
				recipes.RecipeName = Console.ReadLine();

				ingredientCollection();
				recipeStepCollection();

				Console.WriteLine(recipes.displayRecipe());

				recipeScale();

				Console.WriteLine(recipes.displayRecipe());

				if (askToResetRecipe())
				{
					ResetRecipe();
				}
				else
				{
					exitRequested = true;
				}
			}
		}

		private static void ingredientCollection()
		{
			Console.WriteLine("----Ingredient Collection----");
			Console.WriteLine("How many ingredients are there in your recipe?\nPlease enter here:");
			while (recipes.IngredientCount <= 0)
			{
				try
				{
					recipes.IngredientCount = int.Parse(Console.ReadLine());
				}
				catch (Exception ex)
				{
					Console.WriteLine("Please enter a valid number.");
				}
			}

			ingredients = new Ingredient[recipes.IngredientCount];

			for (int step = 0; step < recipes.IngredientCount; step++)
			{
				Console.Write("\nPlease enter the name of the ingredient: ");
				string ingredientName = Console.ReadLine();
				Console.Write("\nPlease enter the amount of ingredient in number form:");
				double ingredientAmount = double.Parse(Console.ReadLine());
				Console.Write("\nPlease enter the measurement type of the ingredient: ");
				string ingredientMeasurement = Console.ReadLine();

				ingredients[step] = new Ingredient(ingredientName, ingredientAmount, ingredientMeasurement);
			}

			recipes.setIngredients(ingredients);
		}

		private static void recipeStepCollection()
		{
			Console.WriteLine("----Step Collection----");
			Console.Write("Please enter the number of steps: ");
			while (recipes.StepCount <= 0)
			{
				try
				{
					recipes.StepCount = int.Parse(Console.ReadLine());
				}
				catch (Exception ex)
				{
					Console.WriteLine("Please enter a valid number.");
				}
			}

			recipeSteps = new string[recipes.StepCount];

			for (int step = 0; step < recipes.StepCount; step++)
			{
				Console.WriteLine("Please enter the directions for step "+ step + 1);
				recipeSteps[step] = Console.ReadLine();
			}

			recipes.setRecipeSteps(recipeSteps);
		}

		private static void recipeScale()
		{
			Console.WriteLine("Would you like to scale the recipe?\n1. Yes\n2. No");

			if (Console.ReadLine() == "1")
			{
				Console.WriteLine("Please enter a number to multiply the recipe by:");
				recipes.setScale(double.Parse(Console.ReadLine()));
			}
		}

		private static bool askToResetRecipe()
		{
			Console.WriteLine("Would you like to reset the recipe and start again?\n1. Yes\n2. No");

			return Console.ReadLine() == "1";
		}

		private static void ResetRecipe()
		{
			recipes.Reset(); // Implement a method in Recipe class to reset its state
			Console.WriteLine("Recipe has been reset. Let's start again!");
		}
	}
}
