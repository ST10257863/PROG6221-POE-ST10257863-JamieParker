using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeManagerWPF
{
	public partial class AddStepWindow : Window
	{
		public string StepDescription
		{
			get; private set;
		}

		public AddStepWindow()
		{
			InitializeComponent();
			// Set placeholder text when window is initialized
			SetPlaceholder(StepDescriptionTextBox, "Step Description");
		}

		public AddStepWindow(string step)
		{
			InitializeComponent();
			// Initialize text box with existing step description
			StepDescriptionTextBox.Text = step;
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
				// Set StepDescription to the current text in StepDescriptionTextBox
				StepDescription = StepDescriptionTextBox.Text;
				this.DialogResult = true; // Set DialogResult to true and close window
				this.Close();
			}
			else
			{
				MessageBox.Show("Please fill in the step description correctly.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		// Validates that StepDescriptionTextBox is not empty and does not contain the placeholder text
		private bool ValidateInputs()
		{
			return !string.IsNullOrWhiteSpace(StepDescriptionTextBox.Text) && StepDescriptionTextBox.Text != "Step Description";
		}
	}
}
