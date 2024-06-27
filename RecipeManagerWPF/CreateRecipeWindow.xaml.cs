using PROG6221_POE_ST10257863_JamieParker.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class CreateRecipeWindow : Window
	{
		private List<Ingredient> ingredients; // List to store ingredients of the recipe
		private List<string> steps; // List to store steps of the recipe
		private Recipe currentRecipe; // Current recipe being edited

		// Event declaration to notify listeners when a recipe is edited
		public event EventHandler<Recipe> RecipeEdited;

		// Property to get the newly created or edited recipe
		public Recipe NewRecipe
		{
			get; private set;
		}

		// Constructor for creating a new recipe
		public CreateRecipeWindow()
		{
			InitializeComponent();
			ingredients = new List<Ingredient>(); // Initialize ingredients list
			steps = new List<string>(); // Initialize steps list
			SetPlaceholder(RecipeNameTextBox, "Recipe Name"); // Set placeholder for recipe name text box
		}

		// Constructor for editing an existing recipe
		public CreateRecipeWindow(Recipe recipe) : this()
		{
			currentRecipe = recipe;
			RecipeNameTextBox.Text = recipe.RecipeName; // Display current recipe name
			ingredients = recipe.Ingredients; // Load ingredients from current recipe
			steps = recipe.RecipeSteps; // Load steps from current recipe

			// Populate ingredients ListBox with ingredient names
			foreach (var ingredient in ingredients)
			{
				IngredientsListBox.Items.Add(ingredient.Name);
			}

			// Populate steps ListBox with step descriptions
			foreach (var step in steps)
			{
				StepsListBox.Items.Add(step);
			}
		}

		// Sets placeholder text and changes text color to gray if text box is empty
		private void SetPlaceholder(TextBox textBox, string placeholder)
		{
			if (string.IsNullOrWhiteSpace(textBox.Text))
			{
				textBox.Text = placeholder;
				textBox.Foreground = Brushes.Gray;
			}
		}

		// Removes placeholder text and changes text color to black when text box gets focus
		private void RemovePlaceholder(TextBox textBox, string placeholder)
		{
			if (textBox.Text == placeholder)
			{
				textBox.Text = string.Empty;
				textBox.Foreground = Brushes.Black;
			}
		}

		// Event handler for when text box gets focus
		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			RemovePlaceholder(textBox, "Recipe Name");
		}

		// Event handler for when text box loses focus
		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			SetPlaceholder(textBox, "Recipe Name");
		}

		// Event handler for Add Ingredient button click
		private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
		{
			var addIngredientWindow = new AddIngredientWindow();
			if (addIngredientWindow.ShowDialog() == true)
			{
				ingredients.Add(addIngredientWindow.NewIngredient); // Add new ingredient to list
				IngredientsListBox.Items.Add(addIngredientWindow.NewIngredient.Name); // Add ingredient name to ListBox
			}
		}

		// Event handler for Add Step button click
		private void AddStepButton_Click(object sender, RoutedEventArgs e)
		{
			var addStepWindow = new AddStepWindow();
			if (addStepWindow.ShowDialog() == true)
			{
				steps.Add(addStepWindow.StepDescription); // Add new step to list
				StepsListBox.Items.Add(addStepWindow.StepDescription); // Add step description to ListBox
			}
		}

		// Event handler for Save Recipe button click
		private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			if (currentRecipe == null)
			{
				// Create new recipe object if editing a new recipe
				NewRecipe = new Recipe
				{
					RecipeName = RecipeNameTextBox.Text,
					Ingredients = ingredients,
					RecipeSteps = steps
				};
			}
			else
			{
				// Update current recipe object if editing an existing recipe
				currentRecipe.RecipeName = RecipeNameTextBox.Text;
				currentRecipe.Ingredients = ingredients;
				currentRecipe.RecipeSteps = steps;
				NewRecipe = currentRecipe;
			}

			// Trigger the RecipeEdited event to notify listeners
			RecipeEdited?.Invoke(this, NewRecipe);

			this.DialogResult = true; // Set DialogResult to true and close window
			this.Close();
		}

		// Event handler for Delete Ingredient button click
		private void DeleteIngredientButton_Click(object sender, RoutedEventArgs e)
		{
			if (IngredientsListBox.SelectedItem != null)
			{
				var selectedItem = IngredientsListBox.SelectedItem.ToString();
				var ingredientToRemove = ingredients.Find(i => i.Name == selectedItem);
				ingredients.Remove(ingredientToRemove); // Remove ingredient from list
				IngredientsListBox.Items.Remove(selectedItem); // Remove ingredient from ListBox
			}
		}

		// Event handler for Edit Ingredient button click
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
					ingredients[index] = editIngredientWindow.NewIngredient; // Update ingredient in list

					// Update ListBox display with edited ingredient name
					IngredientsListBox.Items[IngredientsListBox.SelectedIndex] = editIngredientWindow.NewIngredient.Name;
				}
			}
		}

		// Event handler for Delete Step button click
		private void DeleteStepButton_Click(object sender, RoutedEventArgs e)
		{
			if (StepsListBox.SelectedItem != null)
			{
				steps.RemoveAt(StepsListBox.SelectedIndex); // Remove step from list
				StepsListBox.Items.RemoveAt(StepsListBox.SelectedIndex); // Remove step from ListBox
			}
		}

		// Event handler for Edit Step button click
		private void EditStepButton_Click(object sender, RoutedEventArgs e)
		{
			if (StepsListBox.SelectedItem != null)
			{
				var selectedStep = StepsListBox.SelectedItem.ToString();
				var stepToEdit = steps[StepsListBox.SelectedIndex];

				var editStepWindow = new AddStepWindow(stepToEdit);
				if (editStepWindow.ShowDialog() == true)
				{
					steps[StepsListBox.SelectedIndex] = editStepWindow.StepDescription; // Update step in list

					// Update ListBox display with edited step description
					StepsListBox.Items[StepsListBox.SelectedIndex] = editStepWindow.StepDescription;
				}
			}
		}
	}
}
