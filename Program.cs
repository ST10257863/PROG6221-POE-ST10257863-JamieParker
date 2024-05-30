using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;
		private static List<Recipe> recipes = new List<Recipe>();
		private static Recipe currentRecipe;

		static void Main(string[] args)
		{
			bool exitRequested = false;
			while (!exitRequested)
			{
				string selection = DisplayMainMenu();
				switch (selection)
				{
					case "1":
						CreateNewRecipe();
						break;
					case "2":
						SelectRecipe();
						break;
					case "3":
						DisplayAllRecipes();
						break;
					case "4":
						exitRequested = true;
						break;
					default:
						Console.WriteLine("Invalid selection. Please enter a number between 1 and 4.");
						break;
				}
			}
		}

		private static string DisplayMainMenu()
		{
			Console.Clear();
			Console.WriteLine("Please select an option:");
			Console.WriteLine("1. Create new recipe");
			Console.WriteLine("2. Select recipe");
			Console.WriteLine("3. Display all recipes");
			Console.WriteLine("4. Exit");
			return Console.ReadLine();
		}

		private static void CreateNewRecipe()
		{
			currentRecipe = new Recipe();
			recipes.Add(currentRecipe);
			currentRecipe.CalorieCountExceeded += NotifyCalorieCountExceeded;

			bool finishedCreating = false;
			while (!finishedCreating)
			{
				string createSelection = DisplayCreateMenu();
				switch (createSelection)
				{
					case "1":
						EnterRecipeName();
						break;
					case "2":
						CollectIngredients();
						break;
					case "3":
						CollectRecipeSteps();
						break;
					case "4":
						finishedCreating = true;
						break;
					default:
						Console.WriteLine("Invalid selection. Please enter a number between 1 and 4.");
						break;
				}
			}
		}

		private static string DisplayCreateMenu()
		{
			Console.Clear();
			Console.WriteLine("What would you like to do?");
			Console.WriteLine("1. Enter recipe name");
			Console.WriteLine("2. Enter ingredients");
			Console.WriteLine("3. Enter steps");
			Console.WriteLine("4. Finish creating recipe");
			return Console.ReadLine();
		}

		private static void EnterRecipeName()
		{
			Console.Clear();
			Console.WriteLine("Please enter the recipe name: ");
			currentRecipe.RecipeName = Console.ReadLine();
		}

		private static void SelectRecipe()
		{
			Console.Clear();
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes found, please create a recipe to begin.");
				Console.ReadLine();
				return;
			}
			recipes.Sort((r1, r2) => r1.RecipeName.CompareTo(r2.RecipeName));
			Console.WriteLine("Here are the available recipes:");
			foreach (var recipe in recipes)
			{
				Console.WriteLine(recipe.RecipeName);
			}
			Console.WriteLine("Please enter the recipe name to select: ");
			string recipeName = Console.ReadLine();
			currentRecipe = recipes.Find(recipe => recipe.RecipeName == recipeName);
			if (currentRecipe != null)
			{
				EditRecipe();
			}
			else
			{
				Console.WriteLine("Recipe not found.");
			}
		}

		private static void EditRecipe()
		{
			bool finishedEditing = false;
			while (!finishedEditing)
			{
				Console.Clear();
				Console.WriteLine("What would you like to do with this recipe?");
				Console.WriteLine("1. Display recipe");
				Console.WriteLine("2. Scale recipe");
				Console.WriteLine("3. Reset scale");
				Console.WriteLine("4. Delete recipe");
				Console.WriteLine("5. Go back to main menu");

				string editSelection = Console.ReadLine();
				switch (editSelection)
				{
					case "1":
						DisplayRecipe();
						break;
					case "2":
						ScaleRecipe();
						break;
					case "3":
						ResetRecipe();
						break;
					case "4":
						DeleteRecipe();
						finishedEditing = true;
						break;
					case "5":
						finishedEditing = true;
						break;
					default:
						Console.WriteLine("Invalid selection. Please enter a number between 1 and 5.");
						break;
				}
			}
		}

		private static void DisplayRecipe()
		{
			Console.Clear();
			Console.WriteLine(currentRecipe.displayRecipe());
			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
		}

		private static void ScaleRecipe()
		{
			Console.Clear();
			double scale = ReadDoubleFromConsole("Please enter a number to multiply the recipe by:");
			currentRecipe.setScale(scale);
			Console.WriteLine(currentRecipe.displayRecipe());
			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
		}

		private static void ResetRecipe()
		{
			Console.Clear();
			currentRecipe.ResetScale();
			Console.WriteLine(currentRecipe.displayRecipe());
			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
		}

		private static void DeleteRecipe()
		{
			Console.WriteLine("Are you sure you would like to delete the recipe?\n1. Yes 2. No");
			if (Console.ReadLine() == "1")
			{
				recipes.Remove(currentRecipe);
				currentRecipe = null;
				Console.WriteLine("Recipe has been deleted.");
			}
		}

		private static void DisplayAllRecipes()
		{
			Console.Clear();
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes found, please create a recipe to begin.");
			}
			else
			{
				foreach (var recipe in recipes)
				{
					Console.WriteLine(recipe.displayRecipe());
				}
			}
		}

		public static void NotifyCalorieCountExceeded(int calories)
		{
			Console.WriteLine($"Alert: The calorie count has exceeded the limit! Current count: {calories}");
		}

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

		private static void CollectIngredients()
		{
			Console.WriteLine("Please enter the number of ingredients: ");
			currentRecipe.IngredientCount = ReadIntFromConsole("");
			ingredients = new Ingredient[currentRecipe.IngredientCount];
			for (int i = 0; i < currentRecipe.IngredientCount; i++)
			{
				ingredients[i] = new Ingredient();
				Console.WriteLine("Please enter the ingredient name: ");
				ingredients[i].Name = Console.ReadLine();
				Console.WriteLine("Please enter the ingredient amount: ");
				ingredients[i].Amount = ReadDoubleFromConsole("");
				Console.WriteLine("Please enter the ingredient measurement: ");
				ingredients[i].Measurment = Console.ReadLine();
				Console.WriteLine("Please enter the ingredient calories: ");
				ingredients[i].Calories = ReadDoubleFromConsole("");
			}
			currentRecipe.setIngredients(ingredients);
		}

		private static void CollectRecipeSteps()
		{
			Console.WriteLine("----Step Collection----");
			currentRecipe.StepCount = ReadIntFromConsole("Please enter the number of steps: ");
			recipeSteps = new string[currentRecipe.StepCount];
			for (int step = 0; step < currentRecipe.StepCount; step++)
			{
				Console.WriteLine("Please enter the directions for step " + (step + 1));
				recipeSteps[step] = Console.ReadLine();
			}
			currentRecipe.setRecipeSteps(recipeSteps);
		}
	}
}
