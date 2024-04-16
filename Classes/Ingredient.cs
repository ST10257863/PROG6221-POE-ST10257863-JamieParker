using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	internal class Ingredient
	{
		private string name;
		private double amount;
		private string measurment;

		public Ingredient()
		{
		}
		public Ingredient(string name, double amount, string measurment)
		{
			this.name = name;
			this.amount = amount;
			this.measurment = measurment;
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public double Amount
		{
			get
			{
				return amount;
			}
			set
			{
				amount = value;
			}
		}
		public string Measurment
		{
			get
			{
				return measurment;
			}
			set
			{
				measurment = value;
			}
		}

		//public string fetchIngredient()
		//{
		//	string ingredient;
		//	ingredient = (this.name + " " + this.amount + this.measurment);
		//	return ingredient;
		//}
	}
}
