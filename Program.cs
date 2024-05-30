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
			// Subscribe to the CalorieCountExceeded event
			recipes.CalorieCountExceeded += NotifyCalorieCountExceeded;

			// Main loop for the application
			bool exitRequested = false;
			while (!exitRequested)
			{
				// Display the main menu and handle user selection
				bool validSelection = false;
				while (!validSelection)
				{
					// Clear the console for a clean display
					Console.Clear();

					// Display the main menu
					Console.WriteLine("Please select an option:");
					Console.WriteLine("1. Enter recipe");
					Console.WriteLine("2. Select recipe");
					Console.WriteLine("3. Display all recipes");
					Console.WriteLine("4. Exit");

					// Handle the user's selection
					string selection = Console.ReadLine();
					switch (selection)
					{
						case "1":
							// Enter recipe
							bool validRecipeSelection = false;
							while (!validRecipeSelection)
							{
								// Clear the console for a clean display
								Console.Clear();

								// Display the recipe menu
								Console.WriteLine("Please select an option:");
								Console.WriteLine("1. Enter recipe name");
								Console.WriteLine("2. Enter ingredients");
								Console.WriteLine("3. Enter recipe steps");
								Console.WriteLine("4. Scale recipe");
								Console.WriteLine("5. Back to main menu");

								// Handle the user's selection
								string recipeSelection = Console.ReadLine();
								switch (recipeSelection)
								{
									case "1":
										// Enter recipe name
										Console.Clear();
										Console.WriteLine("Please enter the recipe name: ");
										recipes.RecipeName = Console.ReadLine();
										break;
									case "2":
										// Enter ingredients
										Console.Clear();
										ingredientCollection();
										break;
									case "3":
										// Enter recipe steps
										Console.Clear();
										recipeStepCollection();
										break;
									case "4":
										// Scale recipe
										Console.Clear();
										recipeScale();
										Console.WriteLine(recipes.displayRecipe());
										break;
									case "5":
										// Back to main menu
										validRecipeSelection = true;
										break;
									default:
										Console.WriteLine("Invalid selection. Please enter a number between 1 and 5.");
										break;
								}
							}
							validSelection = true;
							break;
						case "2":
							// Select recipe
							Console.Clear();
							Console.WriteLine("Please enter the recipe name to select: ");
							string recipeName = Console.ReadLine();
							// Add code here to select and display the recipe based on the entered name
							validSelection = true;
							break;
						case "3":
							// Display all recipes
							Console.Clear();
							// Add code here to display all recipes
							validSelection = true;
							break;
						case "4":
							// Exit
							exitRequested = true;
							validSelection = true;
							break;
						default:
							Console.WriteLine("Invalid selection. Please enter a number between 1 and 4.");
							break;
					}
				}
			}
		}

		// Define a method that matches the CalorieCountHandler delegate signature
		public static void NotifyCalorieCountExceeded(int calories)
		{
			Console.WriteLine($"Alert: The calorie count has exceeded the limit! Current count: {calories}");
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
			Console.WriteLine("Please enter the number of ingredients: ");
			recipes.IngredientCount = Convert.ToInt32(Console.ReadLine());
			ingredients = new Ingredient[recipes.IngredientCount];
			for (int i = 0; i < recipes.IngredientCount; i++)
			{
				ingredients[i] = new Ingredient();
				Console.WriteLine("Please enter the ingredient name: ");
				ingredients[i].Name = Console.ReadLine();
				Console.WriteLine("Please enter the ingredient amount: ");
				ingredients[i].Amount = Convert.ToDouble(Console.ReadLine());
				Console.WriteLine("Please enter the ingredient measurement: ");
				ingredients[i].Measurment = Console.ReadLine();
				Console.WriteLine("Please enter the ingredient calories: ");
				ingredients[i].Calories = Convert.ToDouble(Console.ReadLine());
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
