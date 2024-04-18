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
