using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		// Declare the ingredients and recipe steps arrays, and the Recipe object
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;
		private static Recipe recipes = new Recipe();

		static void Main(string[] args)
		{
			bool exitRequested = false;
			while (!exitRequested)
			{
				// Welcome message and recipe name input
				Console.WriteLine("Welcome to PROG6221 Recipe Keeper!");
				Console.WriteLine("Please enter the recipe name: ");
				recipes.RecipeName = Console.ReadLine();

				// Collect ingredients and recipe steps
				ingredientCollection();
				recipeStepCollection();

				// Display the recipe
				Console.WriteLine(recipes.displayRecipe());

				// Ask for recipe scaling
				recipeScale();
				Console.WriteLine(recipes.displayRecipe());

				// Ask to reset the recipe
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

		// Method to read an integer from console with validation
		private static int ReadIntFromConsole(string prompt)
		{
			int result;
			Console.Write(prompt);
			while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
			{
				Console.WriteLine("Please enter a valid number.");
			}
			return result;
		}

		// Method to read a double from console with validation
		private static double ReadDoubleFromConsole(string prompt)
		{
			double result;
			Console.Write(prompt);
			while (!double.TryParse(Console.ReadLine(), out result) || result <= 0)
			{
				Console.WriteLine("Please enter a valid number.");
			}
			return result;
		}

		// Method to collect ingredients
		private static void ingredientCollection()
		{
			Console.WriteLine("----Ingredient Collection----");
			recipes.IngredientCount = ReadIntFromConsole("How many ingredients are there in your recipe?\nPlease enter here: ");
			ingredients = new Ingredient[recipes.IngredientCount];
			for (int step = 0; step < recipes.IngredientCount; step++)
			{
				Console.Write("\nPlease enter the name of the ingredient: ");
				string ingredientName = Console.ReadLine();
				double ingredientAmount = ReadDoubleFromConsole("\nPlease enter the amount of ingredient in number form:");
				Console.Write("\nPlease enter the measurement type of the ingredient: ");
				string ingredientMeasurement = Console.ReadLine();
				ingredients[step] = new Ingredient { Name = ingredientName, Amount = ingredientAmount, Measurment = ingredientMeasurement };
			}
			recipes.setIngredients(ingredients);
		}

		// Method to collect recipe steps
		private static void recipeStepCollection()
		{
			Console.WriteLine("----Step Collection----");
			recipes.StepCount = ReadIntFromConsole("Please enter the number of steps: ");

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
				Console.WriteLine("Please enter the directions for step " + step + 1);
				recipeSteps[step] = Console.ReadLine();
			}
			recipes.setRecipeSteps(recipeSteps);
		}

		// Method to scale the recipe
		private static void recipeScale()
		{
			Console.WriteLine("Would you like to scale the recipe?\n1. Yes\n2. No");
			string input = Console.ReadLine();
			if (input == "1")
			{
				double scale = ReadDoubleFromConsole("Please enter a number to multiply the recipe by:");
				recipes.setScale(scale);
			}
			else if (input != "2")
			{
				Console.WriteLine("Invalid option. Please enter 1 or 2.");
			}
		}

		// Method to ask if the user wants to reset the recipe
		private static bool askToResetRecipe()
		{
			Console.WriteLine("Would you like to reset the recipe and start again?\n1. Yes\n2. No");
			return Console.ReadLine() == "1";
		}

		// Method to reset the recipe
		private static void ResetRecipe()
		{
			recipes.Reset(); // Implement a method in Recipe class to reset its state
			Console.WriteLine("Recipe has been reset. Let's start again!");
		}
	}
}
