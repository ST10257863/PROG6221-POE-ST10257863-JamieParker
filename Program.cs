using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		private static string recipeName;
		private static int ingredientCount;
		private static int stepCount;
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;

		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to PROG6221 Reciper Keeper!");
			Console.WriteLine("Please enter the reciper name: ");
			recipeName = Console.ReadLine();
			ingredientCollection();
			recipeStepCollection();
			displayRecipe();

		}
		private static void ingredientCollection()
		{
			Console.WriteLine("----Ingredient Collection----");
			Console.WriteLine("How many ingredients are there in you recipe?\nPlease enter here:");
			while (ingredientCount <= 0)
			{
				try
				{
					ingredientCount = int.Parse(Console.ReadLine());
					
				}
				catch (Exception ex)
				{
					Console.WriteLine("Please enter a number.");
				}
			}
			ingredients = new Ingredient[ingredientCount];


			for (int step = 0; step < ingredientCount; step++)
			{
				Console.Write("\nPlease enter the name of the ingredient: ");
				string ingredientName = Console.ReadLine();
				Console.Write("\nPlease enter the amount of ingredient in number form:");
				double ingredientAmount = double.Parse(Console.ReadLine());
				Console.Write("\nPlease enter the measurment type of the ingredient: ");
				string ingredientMeasurment = Console.ReadLine();
				ingredients[step] = new Ingredient(ingredientName, ingredientAmount, ingredientMeasurment);
			}
		}
		private static void recipeStepCollection()
		{
			Console.WriteLine("----Step Collection----");
			Console.Write("Please enter the number of steps: ");
			while (stepCount <= 0)
			{
				try
				{
					stepCount = int.Parse(Console.ReadLine());
				}
				catch (Exception ex)
				{
					Console.WriteLine("Please enter a number.");
				}
			}
			recipeSteps = new string[stepCount];

			string recipeStepText = "";

			for (int step = 0; step < stepCount; step++)
			{
				Console.WriteLine("Please enter the directions for step " + (step + 1));
				recipeStepText = Console.ReadLine();
				recipeSteps[step] += "\n1." + recipeStepText;
			}
		}

		private static void displayRecipe()
		{
			;
			Console.WriteLine("----" + recipeName + " Recipe----");
			Console.WriteLine("\n\n----Ingredients----");
			for (int step = 0; step < ingredientCount; step++)
			{
				Console.WriteLine(ingredients[step].fetchIngredient());
			}
			Console.WriteLine("\n\n----Recipe Steps----");
			for (int step = 0; step < stepCount; step++)
			{
				Console.WriteLine(recipeSteps[step]);
			}
		}
	}
}