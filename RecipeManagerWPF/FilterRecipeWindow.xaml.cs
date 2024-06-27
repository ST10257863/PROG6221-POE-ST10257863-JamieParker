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

		private void FilterToggleButton_Click(object sender, RoutedEventArgs e)
		{
			// Toggle visibility based on which toggle button is clicked
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

		private void ToggleVisibility(params FrameworkElement[] elements)
		{
			foreach (var element in elements)
			{
				element.Visibility = (element.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
			}
		}

		private void FilterButton_Click(object sender, RoutedEventArgs e)
		{
			FilterName = NameFilterTextBox.Text;
			FilterIngredients = IngredientsFilterTextBox.Text;
			FilterFoodGroup = FoodGroupFilterComboBox.SelectedItem?.ToString();
			FilterMaxCalories = int.TryParse(MaxCaloriesFilterTextBox.Text, out int maxCalories) ? maxCalories : (int?)null;

			DialogResult = true;
			Close();
		}
	}
}
