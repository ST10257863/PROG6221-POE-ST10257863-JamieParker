namespace PROG6221_POE_ST10257863_JamieParker.Classes
{

	public class Ingredient
	{
		public string Name
		{
			get; set;
		}
		public double Amount
		{
			get; set;
		}
		public string Measurement
		{
			get; set;
		}
		public double Calories
		{
			get; set;
		}
		public string FoodGroup
		{
			get; set;
		}

		public Ingredient()
		{
		}

		public Ingredient(string name, double amount, string measurement, double calories, string foodGroup)
		{
			Name = name;
			Amount = amount;
			Measurement = measurement;
			Calories = calories;
			FoodGroup = foodGroup;
		}
	}

}
