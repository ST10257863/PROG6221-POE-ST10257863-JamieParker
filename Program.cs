﻿using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;

namespace PROG6221_POE_ST10257863_JamieParker
{
	internal class Program
	{
		// Declare the ingredients and recipe steps arrays, and the Recipe object
		private static Ingredient[] ingredients;
		private static string[] recipeSteps;
		private static List<Recipe> recipes = new List<Recipe>();
		private static Recipe currentRecipe;

		static void Main(string[] args)
		{
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
					Console.WriteLine("1. Create new recipe");
					Console.WriteLine("2. Select recipe");
					Console.WriteLine("3. Display all recipes");
					Console.WriteLine("4. Exit");

					// Handle the user's selection
					string selection = Console.ReadLine();
					switch (selection)
					{

						case "1":
							// Create new recipe
							currentRecipe = new Recipe();
							recipes.Add(currentRecipe);
							currentRecipe.CalorieCountExceeded += NotifyCalorieCountExceeded;

							bool finishedCreating = false;
							while (!finishedCreating)
							{
								Console.WriteLine("What would you like to do?");
								Console.WriteLine("1. Enter recipe name");
								Console.WriteLine("2. Enter ingredients");
								Console.WriteLine("3. Enter steps");
								Console.WriteLine("4. Scale recipe");
								Console.WriteLine("5. Finish creating recipe");

								string createSelection = Console.ReadLine();
								switch (createSelection)
								{
									case "1":
										Console.Clear();
										Console.WriteLine("Please enter the recipe name: ");
										currentRecipe.RecipeName = Console.ReadLine();
										break;
									case "2":
										Console.Clear();
										ingredientCollection();
										break;
									case "3":
										Console.Clear();
										recipeStepCollection();
										break;
									case "4":
										Console.Clear();
										recipeScale();
										break;
									case "5":
										finishedCreating = true;
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
							recipes.Sort((r1, r2) => r1.RecipeName.CompareTo(r2.RecipeName)); // Sort recipes by name
							Console.WriteLine("Here are the available recipes:");
							foreach (var recipe in recipes)
							{
								Console.WriteLine(recipe.RecipeName);
							}
							Console.WriteLine("Please enter the recipe name to select: ");
							string recipeName = Console.ReadLine();
							currentRecipe = null;
							foreach (var recipe in recipes)
							{
								if (recipe.RecipeName == recipeName)
								{
									currentRecipe = recipe;
									break;
								}
							}
							if (currentRecipe == null)
							{
								Console.WriteLine("Recipe not found.");
							}
							else
							{
								validSelection = true;
							}
							break;
						case "3":
							// Display all recipes
							Console.Clear();
							foreach (var recipe in recipes)
							{
								Console.WriteLine(recipe.displayRecipe());
							}
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
			currentRecipe.IngredientCount = Convert.ToInt32(Console.ReadLine());
			ingredients = new Ingredient[currentRecipe.IngredientCount];
			for (int i = 0; i < currentRecipe.IngredientCount; i++)
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
			currentRecipe.setIngredients(ingredients);
		}

		// Method to collect recipe steps
		private static void recipeStepCollection()
		{
			Console.WriteLine("----Step Collection----");
			currentRecipe.StepCount = ReadIntFromConsole("Please enter the number of steps: ");

			while (currentRecipe.StepCount <= 0)
			{
				try
				{
					currentRecipe.StepCount = int.Parse(Console.ReadLine());
				}
				catch (Exception)
				{
					Console.WriteLine("Please enter a valid number.");
				}
			}
			recipeSteps = new string[currentRecipe.StepCount];
			for (int step = 0; step < currentRecipe.StepCount; step++)
			{
				Console.WriteLine("Please enter the directions for step " + (step + 1));
				recipeSteps[step] = Console.ReadLine();
			}
			currentRecipe.setRecipeSteps(recipeSteps);
		}

		// Method to scale the recipe
		private static void recipeScale()
		{
			Console.WriteLine("Would you like to scale the recipe?\n1. Yes\n2. No");
			string input = Console.ReadLine();
			if (input == "1")
			{
				double scale = ReadDoubleFromConsole("Please enter a number to multiply the recipe by:");
				currentRecipe.setScale(scale);
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
			currentRecipe.Reset(); // Implement a method in Recipe class to reset its state
			Console.WriteLine("Recipe has been reset. Let's start again!");
		}
	}
}
