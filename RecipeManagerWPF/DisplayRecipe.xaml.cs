using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Windows;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class DisplayRecipe : Window
	{
		public DisplayRecipe(Recipe recipe)
		{
			InitializeComponent();

			// Set DataContext to the recipe to enable data binding
			DataContext = recipe;

			// Subscribe to CalorieCountExceeded event
			recipe.CalorieCountExceeded += Recipe_CalorieCountExceeded;

			// Initialize CalorieCountText with initial value
			UpdateCalorieCountText(recipe.TotalCalories);
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
	}
}
