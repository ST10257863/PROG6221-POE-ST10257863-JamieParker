using PROG6221_POE_ST10257863_JamieParker.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class AddIngredientWindow : Window
	{
		public Ingredient NewIngredient
		{
			get; private set;
		}

		public AddIngredientWindow()
		{
			InitializeComponent();
			// Set placeholders for text boxes when window is initialized
			SetPlaceholder(IngredientNameTextBox, "Ingredient Name");
			SetPlaceholder(AmountTextBox, "Amount (e.g., 200)");
			SetPlaceholder(MeasurementTextBox, "Measurement (e.g., g)");
			SetPlaceholder(CaloriesTextBox, "Calories");
		}

		public AddIngredientWindow(Ingredient ingredient)
		{
			InitializeComponent();
			// Initialize text boxes with values from existing ingredient
			IngredientNameTextBox.Text = ingredient.Name;
			AmountTextBox.Text = ingredient.Amount.ToString();
			MeasurementTextBox.Text = ingredient.Measurement;
			CaloriesTextBox.Text = ingredient.Calories.ToString();
			FoodGroupComboBox.SelectedItem = ingredient.FoodGroup;
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
			RemovePlaceholder(textBox, textBox.Text);
		}

		// Event handler for when text box loses focus
		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			SetPlaceholder(textBox, textBox.Text);
		}

		// Event handler for save button click
		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateInputs())
			{
				// Create new Ingredient object from user input
				NewIngredient = new Ingredient
				{
					Name = IngredientNameTextBox.Text,
					Amount = double.Parse(AmountTextBox.Text),
					Measurement = MeasurementTextBox.Text,
					Calories = int.Parse(CaloriesTextBox.Text),
					FoodGroup = FoodGroupComboBox.Text
				};
				// Set DialogResult to true and close window if inputs are valid
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				// Display error message if inputs are invalid
				MessageBox.Show("Please fill in all fields correctly.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		// Validates user inputs for all required fields
		private bool ValidateInputs()
		{
			return !string.IsNullOrWhiteSpace(IngredientNameTextBox.Text) && IngredientNameTextBox.Text != "Ingredient Name" &&
				   !string.IsNullOrWhiteSpace(AmountTextBox.Text) && AmountTextBox.Text != "Amount (e.g., 200)" &&
				   double.TryParse(AmountTextBox.Text, out _) &&
				   !string.IsNullOrWhiteSpace(MeasurementTextBox.Text) && MeasurementTextBox.Text != "Measurement (e.g., g)" &&
				   !string.IsNullOrWhiteSpace(CaloriesTextBox.Text) && CaloriesTextBox.Text != "Calories" &&
				   int.TryParse(CaloriesTextBox.Text, out _) &&
				   !string.IsNullOrWhiteSpace(FoodGroupComboBox.Text);
		}
	}
}
