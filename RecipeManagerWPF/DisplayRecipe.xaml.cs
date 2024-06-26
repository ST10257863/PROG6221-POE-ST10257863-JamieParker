using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Windows;

namespace RecipeManagerWPF
{
	/// <summary>
	/// Interaction logic for DisplayRecipe.xaml
	/// </summary>
	public partial class DisplayRecipe : Window
	{
		public DisplayRecipe(Recipe recipe)
		{
			InitializeComponent();

			// Set DataContext to the recipe to enable data binding
			DataContext = recipe;
			CalorieCountText.Text = $"Total Calories: {recipe.TotalCalories}";

		}
	}
}
