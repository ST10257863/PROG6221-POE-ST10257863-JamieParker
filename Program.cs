using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		//private static string recipeName;
		//private static int ingredientCount;
		//private static int stepCount;
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;
		private static Recipe recipes = new Recipe();

		static void Main(string[] args)
		{
			int exit = 0;
			while (exit != 1)
			{
				Console.WriteLine("Welcome to PROG6221 Reciper Keeper!");
				Console.WriteLine("Please enter the recipe name: ");
				recipes.RecipeName = Console.ReadLine();
				ingredientCollection();
				recipeStepCollection();
				Console.WriteLine(recipes.displayRecipe());
				recipeScale();
				Console.WriteLine(recipes.displayRecipe());
				resetRecipeScale();
				exit = 1;
			}
		}
		private static void ingredientCollection()
		{
			Console.WriteLine("----Ingredient Collection----");
			Console.WriteLine("How many ingredients are there in you recipe?\nPlease enter here:");
			while (recipes.IngredientCount <= 0)
			{
				try
				{
					recipes.IngredientCount = int.Parse(Console.ReadLine());

				}
				catch (Exception ex)
				{
					Console.WriteLine("Please enter a number.");
				}
			}

			ingredients = new Ingredient[recipes.IngredientCount];


			for (int step = 0; step < recipes.IngredientCount; step++)
			{
				Console.Write("\nPlease enter the name of the ingredient: ");
				string ingredientName = Console.ReadLine();
				Console.Write("\nPlease enter the amount of ingredient in number form:");
				double ingredientAmount = double.Parse(Console.ReadLine());
				Console.Write("\nPlease enter the measurment type of the ingredient: ");
				string ingredientMeasurment = Console.ReadLine();

				ingredients[step] = new Ingredient(ingredientName, ingredientAmount, ingredientMeasurment);
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
					Console.WriteLine("Please enter a number.");
				}
			}
			recipeSteps = new string[recipes.StepCount];

			string recipeStepText = "";

			for (int step = 0; step < recipes.StepCount; step++)
			{
				Console.WriteLine("Please enter the directions for step " + (step + 1));
				recipeStepText = Console.ReadLine();
				recipeSteps[step] += "\n" + step + 1 + ". " + recipeStepText;
			}
			recipes.setRecipeSteps(recipeSteps);
		}

		private static void recipeScale()
		{
			Console.WriteLine("Would you like to scale the recipe?\n1. yes\n2. no");
			if (Console.ReadLine() == "1")
			{
				Console.WriteLine("please enter a number to multiply the recipe by.");
				recipes.setScale(double.Parse(Console.ReadLine()));
			}
		}

		private static void resetRecipeScale()
		{
			Console.WriteLine("Would you like to reset scale the recipe?\n1. yes\n2. no");
			if (Console.ReadLine() == "1")
			{
				Console.WriteLine("The recipe scale has been reset.");
				recipes.resetScale();
				Console.WriteLine(recipes.displayRecipe());
			}
		}
	}
}