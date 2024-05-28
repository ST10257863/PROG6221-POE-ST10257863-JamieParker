namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	internal class Ingredient
	{
		public Ingredient()
		{
		}
		public Ingredient(string name, double amount, string measurment)
		{
			this.Name = name;
			this.Amount = amount;
			this.Measurment = measurment;
		}
		public string Name
		{
			get; set;
		}
		public double Amount
		{
			get; set;
		}
		public string Measurment
		{
			get; set;
		}
	}
}
