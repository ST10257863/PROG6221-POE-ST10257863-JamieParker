﻿using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeManagerWPF
{
	public partial class MainWindow : Window
	{
		private List<Recipe> recipes = new List<Recipe>(); // List to hold all recipes
		private List<Recipe> filteredRecipes = new List<Recipe>(); // List to hold filtered recipes

		public MainWindow()
		{
			InitializeComponent();

			// Auto-fill recipes when the window is opened
			AddSampleRecipes();
			ClearDuplicates(); // Remove duplicate recipes if any
			RefreshRecipesList(); // Refresh the recipes list in the UI
		}

		#region Event Handlers

		private void CreateRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			var createRecipeWindow = new CreateRecipeWindow();
			createRecipeWindow.ShowDialog();

			if (createRecipeWindow.NewRecipe != null)
			{
				recipes.Add(createRecipeWindow.NewRecipe); // Add new recipe to the list
				ClearDuplicates(); // Remove duplicates after adding
				RefreshRecipesList(); // Refresh displayed recipes
			}
		}

		private void SelectRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Ensure a recipe is selected
				if (RecipesListBox.SelectedItem != null)
				{
					if (RecipesListBox.SelectedItem is string recipeNameWithCalories)
					{
						// Extract the recipe name from the formatted string
						string selectedRecipeName = recipeNameWithCalories.Split('-')[0].Trim();

						// Find the recipe object in filteredRecipes or recipes
						Recipe selectedRecipe = filteredRecipes.FirstOrDefault(r => r.RecipeName.Equals(selectedRecipeName, StringComparison.OrdinalIgnoreCase));

						if (selectedRecipe != null)
						{
							DisplayRecipeDetails(selectedRecipe); // Display details of the selected recipe
						}
						else
						{
							MessageBox.Show("Selected recipe not found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
						}
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
			filteredRecipes = recipes; // Refresh filtered recipes with all recipes
			RefreshRecipesList();
		}

		private void FillRecipes_Click(object sender, RoutedEventArgs e)
		{
			AddSampleRecipes(); // Add sample recipes
			ClearDuplicates();
			RefreshRecipesList();
		}

		private void FilterRecipes_Click(object sender, RoutedEventArgs e)
		{
			var filterRecipeWindow = new FilterRecipeWindow();
			if (filterRecipeWindow.ShowDialog() == true)
			{
				// Apply the filter based on user selections
				ApplyFilter(filterRecipeWindow.FilterName, filterRecipeWindow.FilterIngredients, filterRecipeWindow.FilterFoodGroup, filterRecipeWindow.FilterMaxCalories);
			}
		}

		private void ClearFilterRecipes_Click(object sender, RoutedEventArgs e)
		{
			ClearDuplicates();
			filteredRecipes = recipes; // Clear filtering and display all recipes
			RefreshRecipesList();
		}

		#endregion

		#region Private Methods

		private void DisplayRecipeDetails(Recipe recipe)
		{
			DisplayRecipe detailsWindow = new DisplayRecipe(recipe); // Display detailed view of the recipe
			detailsWindow.ShowDialog();
		}

		private void RefreshRecipesList()
		{
			ClearDuplicates(); // Remove any duplicates

			// Create a list to hold formatted recipe names with calories
			List<string> recipeNamesWithCalories = new List<string>();

			// Iterate through filtered recipes and format the display string
			foreach (var recipe in filteredRecipes)
			{
				double totalCalories = recipe.Ingredients.Sum(i => i.Calories); // Calculate total calories for each recipe

				// Format the string to display recipe name and total calories
				string recipeNameWithCalories = $"{recipe.RecipeName} - Calories: {totalCalories}";
				recipeNamesWithCalories.Add(recipeNameWithCalories); // Add to list
			}

			// Sort recipe names alphabetically
			recipeNamesWithCalories.Sort();

			// Set ItemsSource of ListBox to sorted recipe names
			RecipesListBox.ItemsSource = recipeNamesWithCalories;
		}
		private void AddSampleRecipes()
		{
			recipes.Add(new Recipe
			{
				RecipeName = "Lasagna",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Ground beef", Amount = 500, Measurement = "g", Calories = 800, FoodGroup = "Protein" },
			new Ingredient { Name = "Lasagna noodles", Amount = 200, Measurement = "g", Calories = 600, FoodGroup = "Grains" },
			new Ingredient { Name = "Tomato sauce", Amount = 400, Measurement = "ml", Calories = 200, FoodGroup = "Vegetables" },
		},
				RecipeSteps = new List<string>
		{
			"Preheat oven to 375°F (190°C).",
			"Cook ground beef in a skillet until browned.",
			"Layer lasagna noodles, meat sauce, and cheese in a baking dish.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Chocolate Cake",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Flour", Amount = 300, Measurement = "g", Calories = 1200, FoodGroup = "Grains" },
			new Ingredient { Name = "Sugar", Amount = 200, Measurement = "g", Calories = 800, FoodGroup = "Sweeteners" },
			new Ingredient { Name = "Cocoa powder", Amount = 100, Measurement = "g", Calories = 300, FoodGroup = "Flavorings" },
		},
				RecipeSteps = new List<string>
		{
			"Preheat oven to 350°F (175°C).",
			"Mix dry ingredients in a bowl.",
			"Combine wet ingredients and mix until smooth.",
			"Bake in preheated oven for 30-35 minutes.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Chicken Stir-Fry",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Chicken breast", Amount = 400, Measurement = "g", Calories = 600, FoodGroup = "Protein" },
			new Ingredient { Name = "Bell peppers", Amount = 300, Measurement = "g", Calories = 150, FoodGroup = "Vegetables" },
			new Ingredient { Name = "Broccoli", Amount = 200, Measurement = "g", Calories = 100, FoodGroup = "Vegetables" },
		},
				RecipeSteps = new List<string>
		{
			"Cut chicken into strips and stir-fry in a hot pan.",
			"Add vegetables and stir-fry until tender.",
			"Season with soy sauce and serve hot.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Caprese Salad",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Tomatoes", Amount = 300, Measurement = "g", Calories = 100, FoodGroup = "Vegetables" },
			new Ingredient { Name = "Fresh mozzarella", Amount = 200, Measurement = "g", Calories = 400, FoodGroup = "Dairy" },
			new Ingredient { Name = "Basil leaves", Amount = 50, Measurement = "g", Calories = 50, FoodGroup = "Herbs" },
		},
				RecipeSteps = new List<string>
		{
			"Slice tomatoes and mozzarella.",
			"Layer them on a plate, alternating with basil leaves.",
			"Drizzle with balsamic vinegar and season with salt and pepper.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Beef Stroganoff",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Beef sirloin", Amount = 500, Measurement = "g", Calories = 1000, FoodGroup = "Protein" },
			new Ingredient { Name = "Onion", Amount = 100, Measurement = "g", Calories = 50, FoodGroup = "Vegetables" },
			new Ingredient { Name = "Mushrooms", Amount = 200, Measurement = "g", Calories = 100, FoodGroup = "Vegetables" },
			new Ingredient { Name = "Sour cream", Amount = 150, Measurement = "ml", Calories = 300, FoodGroup = "Dairy" },
			new Ingredient { Name = "Egg noodles", Amount = 300, Measurement = "g", Calories = 400, FoodGroup = "Grains" },
		},
				RecipeSteps = new List<string>
		{
			"Slice beef thinly and sauté with onions until browned.",
			"Add mushrooms and cook until tender.",
			"Stir in sour cream and serve over cooked egg noodles.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Greek Salad",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Cucumbers", Amount = 300, Measurement = "g", Calories = 50, FoodGroup = "Vegetables" },
			new Ingredient { Name = "Feta cheese", Amount = 150, Measurement = "g", Calories = 300, FoodGroup = "Dairy" },
			new Ingredient { Name = "Kalamata olives", Amount = 100, Measurement = "g", Calories = 150, FoodGroup = "Condiments" },
			new Ingredient { Name = "Red onion", Amount = 100, Measurement = "g", Calories = 50, FoodGroup = "Vegetables" },
			new Ingredient { Name = "Olive oil", Amount = 30, Measurement = "ml", Calories = 250, FoodGroup = "Oils" },
		},
				RecipeSteps = new List<string>
		{
			"Slice cucumbers and red onion.",
			"Combine with feta cheese and olives.",
			"Drizzle with olive oil and season with salt and oregano.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Chicken Parmesan",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Chicken breast", Amount = 500, Measurement = "g", Calories = 700, FoodGroup = "Protein" },
			new Ingredient { Name = "Bread crumbs", Amount = 100, Measurement = "g", Calories = 400, FoodGroup = "Grains" },
			new Ingredient { Name = "Parmesan cheese", Amount = 100, Measurement = "g", Calories = 400, FoodGroup = "Dairy" },
			new Ingredient { Name = "Marinara sauce", Amount = 400, Measurement = "ml", Calories = 200, FoodGroup = "Vegetables" },
		},
				RecipeSteps = new List<string>
		{
			"Coat chicken in bread crumbs and fry until golden brown.",
			"Top with marinara sauce and parmesan cheese.",
			"Bake until cheese is melted and bubbly.",
		},
			});

			recipes.Add(new Recipe
			{
				RecipeName = "Pasta Carbonara",
				Ingredients = new List<Ingredient>
		{
			new Ingredient { Name = "Spaghetti", Amount = 300, Measurement = "g", Calories = 600, FoodGroup = "Grains" },
			new Ingredient { Name = "Bacon", Amount = 150, Measurement = "g", Calories = 500, FoodGroup = "Protein" },
			new Ingredient { Name = "Egg yolks", Amount = 3, Measurement = "pcs", Calories = 150, FoodGroup = "Dairy" },
			new Ingredient { Name = "Parmesan cheese", Amount = 100, Measurement = "g", Calories = 400, FoodGroup = "Dairy" },
		},
				RecipeSteps = new List<string>
		{
			"Cook spaghetti until al dente.",
			"Fry bacon until crispy and drain excess fat.",
			"Whisk egg yolks with parmesan cheese and mix with hot pasta.",
			"Add bacon and season with black pepper.",
		},
			});

			ClearDuplicates();
			filteredRecipes = recipes;
			RefreshRecipesList();
		}


		private void ClearDuplicates()
		{
			// Remove duplicate recipes based on recipe name
			List<Recipe> uniqueRecipes = new List<Recipe>();
			foreach (Recipe recipe in recipes)
			{
				if (!uniqueRecipes.Any(r => r.RecipeName == recipe.RecipeName))
				{
					uniqueRecipes.Add(recipe); // Add unique recipes to the list
				}
			}
			recipes = uniqueRecipes; // Update recipes list without duplicates
		}

		private void ApplyFilter(string name, string ingredients, string foodGroup, int? maxCalories)
		{
			filteredRecipes = new List<Recipe>(); // Clear previous filtered recipes

			foreach (var recipe in recipes)
			{
				// Check if recipe matches filter criteria
				bool matchesName = string.IsNullOrEmpty(name) || recipe.RecipeName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0;

				bool matchesIngredients = string.IsNullOrEmpty(ingredients);
				if (!matchesIngredients)
				{
					string[] ingredientArray = ingredients.Split(',');
					bool anyIngredientMatched = false;

					foreach (var ing in ingredientArray)
					{
						bool ingredientMatched = recipe.Ingredients.Any(i => i.Name.IndexOf(ing.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
						if (ingredientMatched)
						{
							anyIngredientMatched = true;
							break;
						}
					}

					matchesIngredients = anyIngredientMatched;
				}

				bool matchesFoodGroup = string.IsNullOrEmpty(foodGroup);
				if (!matchesFoodGroup)
				{
					string[] foodGroupArray = foodGroup.Split(',');
					bool anyFoodGroupMatched = false;

					foreach (var group in foodGroupArray)
					{
						string searchFoodGroup = group.Trim().ToLower();

						bool foodGroupMatched = recipe.Ingredients.Any(i => i.FoodGroup.Equals(searchFoodGroup, StringComparison.OrdinalIgnoreCase));
						if (foodGroupMatched)
						{
							anyFoodGroupMatched = true;
							break;
						}
					}

					matchesFoodGroup = anyFoodGroupMatched;
				}

				bool matchesMaxCalories = !maxCalories.HasValue || recipe.Ingredients.Sum(i => i.Calories) <= maxCalories;

				// Add recipe to filtered list if all criteria are matched
				if (matchesName && matchesIngredients && matchesFoodGroup && matchesMaxCalories)
				{
					filteredRecipes.Add(recipe);
				}
			}

			RefreshRecipesList(); // Refresh displayed recipes based on filters
		}

		#endregion
	}
}