using System;
using System.Windows;

namespace RecipeManagerWPF
{
	public partial class FilterRecipeWindow : Window
	{
		public string FilterName
		{
			get; private set;
		}
		public string FilterIngredients
		{
			get; private set;
		}
		public string FilterFoodGroup
		{
			get; private set;
		}
		public int? FilterMaxCalories
		{
			get; private set;
		}

		public FilterRecipeWindow()
		{
			InitializeComponent();
		}

		// Event handler for clicking on any filter toggle button
		private void FilterToggleButton_Click(object sender, RoutedEventArgs e)
		{
			// Toggle visibility of corresponding filter controls based on which toggle button is clicked
			if (sender == NameFilterToggleButton)
			{
				ToggleVisibility(NameFilterTextBlock, NameFilterTextBox);
			}
			else if (sender == IngredientsFilterToggleButton)
			{
				ToggleVisibility(IngredientsFilterTextBlock, IngredientsFilterTextBox);
			}
			else if (sender == FoodGroupFilterToggleButton)
			{
				ToggleVisibility(FoodGroupFilterTextBlock, FoodGroupFilterComboBox);
			}
			else if (sender == MaxCaloriesFilterToggleButton)
			{
				ToggleVisibility(MaxCaloriesFilterTextBlock, MaxCaloriesFilterTextBox);
			}
		}

		// Method to toggle visibility of elements
		private void ToggleVisibility(params FrameworkElement[] elements)
		{
			foreach (var element in elements)
			{
				// Toggle visibility between Collapsed and Visible
				element.Visibility = (element.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
			}
		}

		// Event handler for clicking the Filter Button
		private void FilterButton_Click(object sender, RoutedEventArgs e)
		{
			// Assign filter values based on user input
			FilterName = NameFilterTextBox.Text;
			FilterIngredients = IngredientsFilterTextBox.Text;
			FilterFoodGroup = FoodGroupFilterComboBox.SelectedItem?.ToString();
			FilterMaxCalories = int.TryParse(MaxCaloriesFilterTextBox.Text, out int maxCalories) ? maxCalories : (int?)null;

			// Set DialogResult to true to indicate successful filtering and close the window
			DialogResult = true;
			Close();
		}
	}
}
