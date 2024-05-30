using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		// Declare variables
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;
		private static List<Recipe> recipes = new List<Recipe>();
		private static Recipe currentRecipe;

		// Main method
		static void Main(string[] args)
		{
			// Loop until user requests to exit
			bool exitRequested = false;
			while (!exitRequested)
			{
				// Display main menu and get user selection
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

		// Method to display main menu
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

		// Method to create a new recipe
		private static void CreateNewRecipe()
		{
			// Initialize new recipe and add to list
			currentRecipe = new Recipe();
			recipes.Add(currentRecipe);
			currentRecipe.CalorieCountExceeded += NotifyCalorieCountExceeded;

			// Loop until user finishes creating recipe
			bool finishedCreating = false;
			while (!finishedCreating)
			{
				// Display create menu and get user selection
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

		// Method to display create menu
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

		// Method to enter recipe name
		private static void EnterRecipeName()
		{
			Console.Clear();
			Console.WriteLine("Please enter the recipe name: ");
			currentRecipe.RecipeName = Console.ReadLine();
		}

		// Method to select a recipe
		private static void SelectRecipe()
		{
			Console.Clear();
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes found, please create a recipe to begin.");
				Console.ReadLine();
				return;
			}
			// Sort recipes by name
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

		// Method to edit a recipe
		private static void EditRecipe()
		{
			// Loop until user finishes editing
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

		// Method to display a recipe
		private static void DisplayRecipe()
		{
			Console.Clear();
			Console.WriteLine(currentRecipe.displayRecipe());
			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
		}

		// Method to scale a recipe
		private static void ScaleRecipe()
		{
			Console.Clear();
			double scale = ReadDoubleFromConsole("Please enter a number to multiply the recipe by:");
			currentRecipe.setScale(scale);
			Console.WriteLine(currentRecipe.displayRecipe());
			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
		}

		// Method to reset a recipe
		private static void ResetRecipe()
		{
			Console.Clear();
			currentRecipe.ResetScale();
			Console.WriteLine(currentRecipe.displayRecipe());
			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
		}

		// Method to delete a recipe
		private static void DeleteRecipe()
		{
			Console.ForegroundColor = ConsoleColor.Red; // Change text color to red
			Console.WriteLine("Are you sure you would like to delete the recipe?\n1. Yes 2. No");
			Console.ResetColor(); // Reset text color
			if (Console.ReadLine() == "1")
			{
				recipes.Remove(currentRecipe);
				currentRecipe = null;
				Console.WriteLine("Recipe has been deleted.");
			}
		}

		// Method to display all recipes
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

		// Method to notify when calorie count is exceeded
		public static void NotifyCalorieCountExceeded(int calories)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow; // Change text color to dark yellow (closest to orange)
			Console.WriteLine($"Alert: The calorie count has exceeded the limit! Current count: {calories}");
			Console.ResetColor(); // Reset text color
		}

		// Method to read an integer from console
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

		// Method to read a double from console
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
				Console.WriteLine("Please select the ingredient food group: ");
				Console.WriteLine("1. Dairy");
				Console.WriteLine("2. Meat");
				Console.WriteLine("3. Vegetables");
				Console.WriteLine("4. Fruits");
				Console.WriteLine("5. Grains");
				string foodGroupSelection = Console.ReadLine();
				switch (foodGroupSelection)
				{
					case "1":
						ingredients[i].FoodGroup = "Dairy";
						break;
					case "2":
						ingredients[i].FoodGroup = "Meat";
						break;
					case "3":
						ingredients[i].FoodGroup = "Vegetables";
						break;
					case "4":
						ingredients[i].FoodGroup = "Fruits";
						break;
					case "5":
						ingredients[i].FoodGroup = "Grains";
						break;
					default:
						Console.WriteLine("Invalid selection. Please enter a number between 1 and 5.");
						i--; // Repeat the loop for the same ingredient
						continue;
				}
			}
			currentRecipe.setIngredients(ingredients);
		}

		// Method to collect recipe steps
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
