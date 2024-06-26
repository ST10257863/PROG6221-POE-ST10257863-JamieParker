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
			SetPlaceholder(StepDescriptionTextBox, "Step Description");
		}

		public AddStepWindow(string step)
		{
			InitializeComponent();
			StepDescriptionTextBox.Text = step;
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
				StepDescription = StepDescriptionTextBox.Text;
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				MessageBox.Show("Please fill in the step description correctly.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private bool ValidateInputs()
		{
			return !string.IsNullOrWhiteSpace(StepDescriptionTextBox.Text) && StepDescriptionTextBox.Text != "Step Description";
		}
	}
}
