using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeManagerWPF
{
	public partial class MainWindow : Window
	{
		private List<Recipe> recipes = new List<Recipe>();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CreateRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			// Logic to create a new recipe
			// You might want to open a new window or a dialog to enter recipe details
			var createRecipeWindow = new CreateRecipeWindow();
			createRecipeWindow.ShowDialog();
			if (createRecipeWindow.NewRecipe != null)
			{
				recipes.Add(createRecipeWindow.NewRecipe);
			}
		}

		private void SelectRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Logic to select a recipe
				if (RecipesListBox.SelectedItem is string selectedRecipeName)
				{
					Recipe selectedRecipe = recipes.FirstOrDefault(r => r.RecipeName == selectedRecipeName);
					if (selectedRecipe != null)
					{
						// Debug output for verification
						Console.WriteLine($"Selected Recipe: {selectedRecipe.RecipeName}");

						// Pass selectedRecipe to a new window for display
						DisplayRecipe detailsWindow = new DisplayRecipe(selectedRecipe);
						detailsWindow.ShowDialog(); // Show the window as a dialog
					}
				}
				else
				{
					MessageBox.Show("Please select a recipe first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void RefreshRecipesButton_Click(object sender, RoutedEventArgs e)
		{
			ClearDuplicates();
			// Sort the unique recipe names
			List<string> recipeNames = new List<string>();
			foreach (Recipe recipe in recipes)
			{
				recipeNames.Add(recipe.RecipeName);
			}
			recipeNames.Sort();

			// Display the sorted recipe names in the ListBox
			RecipesListBox.ItemsSource = recipeNames;
		}

		private void FillRecipes_Click(object sender, RoutedEventArgs e)
		{
			recipes.Add(new Recipe
			{
				RecipeName = "Lasagna",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Ground beef", Amount = 500, Measurement = "g", Calories = 800 },
			new Ingredient { Name = "Lasagna noodles", Amount = 200, Measurement = "g", Calories = 600 },
			new Ingredient { Name = "Tomato sauce", Amount = 400, Measurement = "ml", Calories = 200 },
            // Add more ingredients as needed
        },
				RecipeSteps = new List<string>
		{
			"Preheat oven to 375°F (190°C).",
			"Cook ground beef in a skillet until browned.",
			"Layer lasagna noodles, meat sauce, and cheese in a baking dish.",
            // Add more steps as needed
        }
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Chocolate Cake",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Flour", Amount = 300, Measurement = "g", Calories = 1200 },
			new Ingredient { Name = "Sugar", Amount = 200, Measurement = "g", Calories = 800 },
			new Ingredient { Name = "Cocoa powder", Amount = 100, Measurement = "g", Calories = 300 },
            // Add more ingredients as needed
        },
				RecipeSteps = new List<string>
		{
			"Preheat oven to 350°F (175°C).",
			"Mix dry ingredients in a bowl.",
			"Combine wet ingredients and mix until smooth.",
			"Bake in preheated oven for 30-35 minutes.",
            // Add more steps as needed
        }
			});

			// Add more recipes as needed
			List<string> recipeNames = new List<string>();
			foreach (Recipe recipe in recipes)
			{
				recipeNames.Add(recipe.RecipeName);
				recipeNames.Sort();
			}
			RecipesListBox.ItemsSource = recipeNames;

		}

		public void ClearDuplicates()
		{

			// Create a list to hold unique recipes
			List<Recipe> uniqueRecipes = new List<Recipe>();

			// Iterate over the recipes to remove duplicates
			foreach (Recipe recipe in recipes)
			{
				bool isDuplicate = false;
				foreach (Recipe uniqueRecipe in uniqueRecipes)
				{
					if (uniqueRecipe.RecipeName == recipe.RecipeName)
					{
						isDuplicate = true;
						break;
					}
				}
				if (!isDuplicate)
				{
					uniqueRecipes.Add(recipe);
				}
			}
			recipes.Clear();
			recipes = uniqueRecipes;

		}
	}
}
