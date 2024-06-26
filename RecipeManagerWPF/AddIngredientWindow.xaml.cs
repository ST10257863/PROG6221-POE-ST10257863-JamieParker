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
			SetPlaceholder(IngredientNameTextBox, "Ingredient Name");
			SetPlaceholder(AmountTextBox, "Amount (e.g., 200)");
			SetPlaceholder(MeasurementTextBox, "Measurement (e.g., g)");
			SetPlaceholder(CaloriesTextBox, "Calories");
		}

		public AddIngredientWindow(Ingredient ingredient)
		{
			InitializeComponent();
			IngredientNameTextBox.Text = ingredient.Name;
			AmountTextBox.Text = ingredient.Amount.ToString();
			MeasurementTextBox.Text = ingredient.Measurement;
			CaloriesTextBox.Text = ingredient.Calories.ToString();
			FoodGroupComboBox.SelectedItem = ingredient.FoodGroup;

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
			RemovePlaceholder(textBox, textBox.Text);
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			SetPlaceholder(textBox, textBox.Text);
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateInputs())
			{
				NewIngredient = new Ingredient
				{
					Name = IngredientNameTextBox.Text,
					Amount = double.Parse(AmountTextBox.Text),
					Measurement = MeasurementTextBox.Text,
					Calories = int.Parse(CaloriesTextBox.Text),
					FoodGroup = FoodGroupComboBox.Text
				};
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				MessageBox.Show("Please fill in all fields correctly.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

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
