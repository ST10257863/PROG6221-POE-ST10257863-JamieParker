using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		private static string recipeName;
		private static int ingredientCount;
		private static int stepCount;
		private static string[] ingredientName;
		private static string[] ingredientAmount;
		private static string[] ingredientMeasurment;
		private static string[] recipeSteps;

		static void Main(string[] args)
		{
			String ingredient;
			String amount;
			String measurment;
			Console.WriteLine("Welcome to PROG6221 Reciper Keeper!");
			//Console.WriteLine("Please enter the reciper name: ");
			//ingredientCollection();
			recipeStepCollection();


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
			ingredientName = new string[ingredientCount];
			ingredientAmount = new string[ingredientCount];
			ingredientMeasurment = new string[ingredientCount];

			for (int step = 0; step < ingredientCount; step++)
			{
				Console.Write("\nPlease enter the name of the ingredient: ");
				ingredientName[step] = Console.ReadLine();
				Console.WriteLine("\nPlease enter the amount of ingredient in number form:");
				ingredientAmount[step] = Console.ReadLine();
				Console.WriteLine("\nPlease enter the measurment type of the ingredient: ");
				ingredientMeasurment[step] = Console.ReadLine();
			}
			//for (int step = 0; step < ingredientCount; step++)
			//{
			//	Console.WriteLine(ingredientName[step]);
			//	Console.WriteLine(ingredientAmount[step]);
			//	Console.WriteLine(ingredientMeasurment[step]);
			//}
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

			String recipeStepText = "";
			for (int step = 0; step < stepCount; step++)
			{
				Console.WriteLine("Please enter \"BREAK\" to end the step");
				while (recipeStepText.ToUpper() != "BREAK")
				{
					recipeStepText = Console.ReadLine();
					recipeSteps[step] += recipeStepText;
				}
			}


			Console.WriteLine("\nRecipe Steps:");

			for (int i = 0; i < stepCount - 1; i++)
			{
				Console.WriteLine(recipeSteps[i]);
			}
		}
	}
}