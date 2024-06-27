using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class DisplayRecipe : Window
	{
		private Recipe currentRecipe;

		public DisplayRecipe(Recipe recipe)
		{
			InitializeComponent();
			currentRecipe = recipe;

			// Set DataContext to the recipe to enable data binding
			DataContext = recipe;

			// Subscribe to CalorieCountExceeded event
			recipe.CalorieCountExceeded += Recipe_CalorieCountExceeded;

			// Initialize CalorieCountText with initial value
			UpdateCalorieCountText(recipe.TotalCalories);

			// Set initial items sources
			IngredientsListBox.ItemsSource = currentRecipe.Ingredients;
			StepsListBox.ItemsSource = currentRecipe.RecipeSteps;
		}

		private void IngredientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			HandleListBoxSelectionChange(IngredientsListBox, e);
		}

		private void StepsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			HandleListBoxSelectionChange(StepsListBox, e);
		}

		private void HandleListBoxSelectionChange(ListBox listBox, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				foreach (var item in e.AddedItems)
				{
					ToggleTextColor(listBox, item, true); // Toggle text color to gray when selected
				}
			}
			if (e.RemovedItems.Count > 0)
			{
				foreach (var item in e.RemovedItems)
				{
					ToggleTextColor(listBox, item, false); // Toggle text color back to black when deselected
				}
			}
		}

		private void ToggleTextColor(ListBox listBox, object item, bool isSelected)
		{
			var textBlock = listBox.ItemContainerGenerator.ContainerFromItem(item) as TextBlock;
			if (textBlock != null)
			{
				textBlock.Foreground = isSelected ? Brushes.DarkGreen : Brushes.Black;
			}
		}

		private void Recipe_CalorieCountExceeded(int calories)
		{
			// Update the text whenever CalorieCountExceeded event is triggered
			UpdateCalorieCountText(calories);
		}

		private void UpdateCalorieCountText(double calories)
		{
			if (calories > 300)
			{
				CalorieCountText.Text = $"Warning: Total calories ({calories}) exceed 300!";
				CalorieCountText.Foreground = Brushes.Red; // Change text color to red
			}
			else
			{
				CalorieCountText.Text = $"Total Calories: {calories}";
			}
		}

		private void EditRecipe_Click(object sender, RoutedEventArgs e)
		{
			var editRecipeWindow = new CreateRecipeWindow(currentRecipe);
			editRecipeWindow.RecipeEdited += EditRecipeWindow_RecipeEdited;
			editRecipeWindow.ShowDialog();
		}
		private void EditRecipeWindow_RecipeEdited(object sender, Recipe editedRecipe)
		{
			// Update the current recipe with changes
			currentRecipe = editedRecipe;

			// Refresh DataContext to reflect changes
			DataContext = null; // Reset DataContext
			DataContext = currentRecipe; // Assign new DataContext

			// Update the calorie count text with the new value
			UpdateCalorieCountText(currentRecipe.TotalCalories);

			// Rebind ListBox items sources
			RebindIngredientsListBox();
			RebindStepsListBox();
		}

		private void RebindIngredientsListBox()
		{
			IngredientsListBox.ItemsSource = null; // Clear existing binding
			IngredientsListBox.ItemsSource = currentRecipe.Ingredients; // Rebind to updated collection
		}

		private void RebindStepsListBox()
		{
			StepsListBox.ItemsSource = null; // Clear existing binding
			StepsListBox.ItemsSource = currentRecipe.RecipeSteps; // Rebind to updated collection
		}
	}
}
