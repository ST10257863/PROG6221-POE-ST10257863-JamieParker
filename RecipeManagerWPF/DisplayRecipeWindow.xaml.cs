using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class DisplayRecipe : Window
	{
		private Recipe currentRecipe;
		private double originalScale; // Store the original scale before scaling

		public DisplayRecipe(Recipe recipe)
		{
			InitializeComponent();

			// Initialize with the provided recipe
			currentRecipe = recipe;
			originalScale = recipe.GetScale(); // Store the original scale

			// Set DataContext to the recipe to enable data binding
			DataContext = recipe;

			// Subscribe to CalorieCountExceeded event to handle exceeding calorie counts
			recipe.CalorieCountExceeded += Recipe_CalorieCountExceeded;

			// Initialize CalorieCountText with initial value
			UpdateCalorieCountText(recipe.TotalCalories);

			// Set initial items sources and update for scale
			UpdateIngredientsList();
			UpdateStepsList();
		}

		// Event handler for CalorieCountExceeded event
		private void Recipe_CalorieCountExceeded(int calories)
		{
			// Update the calorie count text whenever CalorieCountExceeded event is triggered
			UpdateCalorieCountText(calories);
		}

		// Updates the displayed calorie count based on the provided value
		private void UpdateCalorieCountText(double calories)
		{
			if (calories > 300)
			{
				CalorieCountText.Text = $"Warning: Total calories ({calories}) exceed 300!";
				CalorieCountText.Foreground = Brushes.Red; // Change text color to red for warning
			}
			else
			{
				CalorieCountText.Text = $"Total Calories: {calories}";
				CalorieCountText.Foreground = Brushes.Black; // Reset text color to black
			}
		}

		// Event handler for Edit Recipe button click
		private void EditRecipe_Click(object sender, RoutedEventArgs e)
		{
			// Open a new window to edit the current recipe
			var editRecipeWindow = new CreateRecipeWindow(currentRecipe);
			editRecipeWindow.RecipeEdited += EditRecipeWindow_RecipeEdited;
			editRecipeWindow.ShowDialog();
		}

		// Event handler for Scale Recipe button click
		private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
		{
			if (ScaleComboBox.SelectedItem != null)
			{
				// Retrieve the selected scale value
				string selectedScale = (ScaleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
				if (double.TryParse(selectedScale, out double scaleValue))
				{
					// Reset to original scale first
					currentRecipe.SetScale(originalScale);

					// Then apply the new scale
					currentRecipe.SetScale(scaleValue);

					// Update DataContext to reflect scaled recipe
					DataContext = null; // Reset DataContext
					DataContext = currentRecipe; // Assign new DataContext

					// Update the calorie count text with the new value
					UpdateCalorieCountText(currentRecipe.TotalCalories);

					// Update the ingredients and steps lists
					UpdateIngredientsList();
					UpdateStepsList();

					// Update originalScale to the current scale
					originalScale = scaleValue;
				}
			}
		}

		// Event handler for recipe edited event from the edit recipe window
		private void EditRecipeWindow_RecipeEdited(object sender, Recipe editedRecipe)
		{
			// Update the current recipe with the edited version
			currentRecipe = editedRecipe;

			// Refresh DataContext to reflect changes
			DataContext = null; // Reset DataContext
			DataContext = currentRecipe; // Assign new DataContext

			// Update the calorie count text with the new value
			UpdateCalorieCountText(currentRecipe.TotalCalories);

			// Update the ingredients and steps lists
			UpdateIngredientsList();
			UpdateStepsList();

			// Update originalScale to the current scale
			originalScale = currentRecipe.GetScale();
		}

		// Update the ingredients list based on the current recipe and scale
		private void UpdateIngredientsList()
		{
			IngredientsListBox.ItemsSource = null;
			IngredientsListBox.ItemsSource = ScaleIngredients(currentRecipe.Ingredients);
		}

		// Update the steps list based on the current recipe
		private void UpdateStepsList()
		{
			StepsListBox.ItemsSource = null;
			StepsListBox.ItemsSource = currentRecipe.RecipeSteps;
		}

		// Event handler for selecting items in the ingredients list box
		private void IngredientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (var item in e.AddedItems)
			{
				ToggleTextColor(IngredientsListBox, item, true); // Toggle text color to gray when selected
			}
		}

		// Event handler for selecting items in the steps list box
		private void StepsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (var item in e.AddedItems)
			{
				ToggleTextColor(StepsListBox, item, true); // Toggle text color to gray when selected
			}
		}

		// Helper method to toggle text color of selected items in a list box
		private void ToggleTextColor(ListBox listBox, object item, bool isSelected)
		{
			var listBoxItem = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
			if (listBoxItem != null)
			{
				var textBlock = FindVisualChild<TextBlock>(listBoxItem);
				if (textBlock != null)
				{
					if (textBlock.Foreground == Brushes.Black)
						textBlock.Foreground = Brushes.DarkGray; // Toggle text color to gray if not already gray
					else if (textBlock.Foreground == Brushes.DarkGray)
						textBlock.Foreground = Brushes.Black; // Toggle text color to black if currently gray
				}
			}
		}

		// Helper method to find a visual child of a specific type within a parent dependency object
		private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				if (child != null && child is T typedChild)
				{
					return typedChild;
				}
				else
				{
					T childOfChild = FindVisualChild<T>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}

		// Scale the ingredients based on the current recipe's scale factor
		private List<Ingredient> ScaleIngredients(List<Ingredient> ingredients)
		{
			// Create a new list to hold scaled ingredients without modifying original
			List<Ingredient> scaledIngredients = new List<Ingredient>();

			foreach (var ingredient in ingredients)
			{
				// Create a new Ingredient object with scaled values for display
				Ingredient scaledIngredient = new Ingredient
				{
					Name = ingredient.Name,
					Amount = ingredient.Amount * currentRecipe.GetScale(), // Scale the amount
					Measurement = ingredient.Measurement,
					Calories = Convert.ToInt32(Math.Round(ingredient.Calories * currentRecipe.GetScale())), // Scale calories
					FoodGroup = ingredient.FoodGroup
				};
				scaledIngredients.Add(scaledIngredient);
			}

			return scaledIngredients;
		}

	}
}
