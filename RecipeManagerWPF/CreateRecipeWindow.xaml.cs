using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class CreateRecipeWindow : Window
	{
		private List<Ingredient> ingredients = new List<Ingredient>();
		private List<string> steps = new List<string>();

		public Recipe NewRecipe
		{
			get; private set;
		}

		public CreateRecipeWindow()
		{
			InitializeComponent();
			SetPlaceholder(RecipeNameTextBox, "Recipe Name");
		}

		private void SetPlaceholder(TextBox textBox, string placeholder)
		{
			if (string.IsNullOrWhiteSpace(textBox.Text))
			{
				textBox.Text = placeholder;
				textBox.Foreground = Brushes.Gray;
			}
		}

		private void RemovePlaceholder(TextBox textBox, string placeholder)
		{
			if (textBox.Text == placeholder)
			{
				textBox.Text = string.Empty;
				textBox.Foreground = Brushes.Black;
			}
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			RemovePlaceholder(textBox, "Recipe Name");
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			SetPlaceholder(textBox, "Recipe Name");
		}

		private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
		{
			var addIngredientWindow = new AddIngredientWindow();
			if (addIngredientWindow.ShowDialog() == true)
			{
				ingredients.Add(addIngredientWindow.NewIngredient);
				IngredientsListBox.Items.Add(addIngredientWindow.NewIngredient.Name);
			}
		}

		private void AddStepButton_Click(object sender, RoutedEventArgs e)
		{
			var addStepWindow = new AddStepWindow();
			if (addStepWindow.ShowDialog() == true)
			{
				steps.Add(addStepWindow.StepDescription);
				StepsListBox.Items.Add(addStepWindow.StepDescription);
			}
		}

		private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			NewRecipe = new Recipe
			{
				RecipeName = RecipeNameTextBox.Text,
				Ingredients = ingredients,
				RecipeSteps = steps
			};
			this.DialogResult = true;
			this.Close();
		}

		private void DeleteIngredientButton_Click(object sender, RoutedEventArgs e)
		{
			if (IngredientsListBox.SelectedItem != null)
			{
				var selectedItem = IngredientsListBox.SelectedItem.ToString();
				var ingredientToRemove = ingredients.Find(i => i.Name == selectedItem);
				ingredients.Remove(ingredientToRemove);
				IngredientsListBox.Items.Remove(selectedItem);
			}
		}

		private void EditIngredientButton_Click(object sender, RoutedEventArgs e)
		{
			if (IngredientsListBox.SelectedItem != null)
			{
				var selectedItem = IngredientsListBox.SelectedItem.ToString();
				var ingredientToEdit = ingredients.Find(i => i.Name == selectedItem);

				var editIngredientWindow = new AddIngredientWindow(ingredientToEdit);
				if (editIngredientWindow.ShowDialog() == true)
				{
					int index = ingredients.IndexOf(ingredientToEdit);
					ingredients[index] = editIngredientWindow.NewIngredient;

					// Update ListBox display if needed
					IngredientsListBox.Items[IngredientsListBox.SelectedIndex] = editIngredientWindow.NewIngredient.Name;
				}
			}
		}

		private void DeleteStepButton_Click(object sender, RoutedEventArgs e)
		{
			if (StepsListBox.SelectedItem != null)
			{
				steps.RemoveAt(StepsListBox.SelectedIndex);
				StepsListBox.Items.RemoveAt(StepsListBox.SelectedIndex);
			}
		}

		private void EditStepButton_Click(object sender, RoutedEventArgs e)
		{
			if (StepsListBox.SelectedItem != null)
			{
				var selectedStep = StepsListBox.SelectedItem.ToString();
				var stepToEdit = steps[StepsListBox.SelectedIndex];

				var editStepWindow = new AddStepWindow(stepToEdit);
				if (editStepWindow.ShowDialog() == true)
				{
					steps[StepsListBox.SelectedIndex] = editStepWindow.StepDescription;

					// Update ListBox display if needed
					StepsListBox.Items[StepsListBox.SelectedIndex] = editStepWindow.StepDescription;
				}
			}
		}
	}
}
