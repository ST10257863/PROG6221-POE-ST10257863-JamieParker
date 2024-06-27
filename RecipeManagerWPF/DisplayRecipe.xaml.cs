using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Linq;
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
			foreach (var item in e.AddedItems)
			{
				ToggleTextColor(IngredientsListBox, item, true); // Toggle text color to gray when selected
			}
		}

		private void StepsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (var item in e.AddedItems)
			{
				ToggleTextColor(StepsListBox, item, true); // Toggle text color to gray when selected
			}
		}

		private void ToggleTextColor(ListBox listBox, object item, bool isSelected)
		{
			var listBoxItem = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
			if (listBoxItem != null)
			{
				var textBlock = FindVisualChild<TextBlock>(listBoxItem);
				if (textBlock != null)
				{

					if (textBlock.Foreground == Brushes.Black)
						textBlock.Foreground = Brushes.DarkGray;
					// Toggle text color to gray if not already gray
					else if (textBlock.Foreground == Brushes.DarkGray)
						textBlock.Foreground = Brushes.Black;

				}
			}
		}

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
			var selectedItems = IngredientsListBox.SelectedItems.Cast<object>().ToList();

			IngredientsListBox.ItemsSource = null; // Clear existing binding
			IngredientsListBox.ItemsSource = currentRecipe.Ingredients; // Rebind to updated collection

			// Reapply selection
			foreach (var item in selectedItems)
			{
				if (IngredientsListBox.Items.Contains(item))
				{
					IngredientsListBox.SelectedItems.Add(item);
					ToggleTextColor(IngredientsListBox, item, true); // Ensure text color is gray
				}
			}
		}

		private void RebindStepsListBox()
		{
			StepsListBox.ItemsSource = null; // Clear existing binding
			StepsListBox.ItemsSource = currentRecipe.RecipeSteps; // Rebind to updated collection
		}
	}
}
