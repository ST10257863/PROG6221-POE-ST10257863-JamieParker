using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Windows;
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
			DataContext = currentRecipe; // Refresh DataContext to reflect changes

			// Unsubscribe from the event to avoid memory leaks
			var editRecipeWindow = sender as CreateRecipeWindow;
			if (editRecipeWindow != null)
			{
				editRecipeWindow.RecipeEdited -= EditRecipeWindow_RecipeEdited;
			}

			// Update the calorie count text with the new value
			UpdateCalorieCountText(currentRecipe.TotalCalories);
		}
	}
}
