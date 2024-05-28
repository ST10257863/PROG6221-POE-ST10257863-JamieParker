using System.Text;

namespace PROG6221_POE_ST10257863_JamieParker.Classes
{
	internal class Recipe
	{

		private Ingredient[] ingredients;
		private string[] recipeSteps;
		private double scale = 1;
		public Recipe()
		{
		}
		public string RecipeName
		{
			get; set;
		}
		public int IngredientCount
		{
			get; set;
		}
		public int StepCount
		{
			get; set;
		}
		public void setIngredients(Ingredient[] ingredients)
		{
			this.ingredients = ingredients;
		}
		public Ingredient getIngredient(int ingredientIndex)
		{
			return ingredients[ingredientIndex];
		}
		public void setRecipeSteps(string[] recipeSteps)
		{
			this.recipeSteps = recipeSteps;
		}
		public string getRecipeStep(int stepIndex)
		{
			return recipeSteps[stepIndex];
		}
		public void setScale(double scale)
		{
			this.scale = scale;
		}
		public double getScale()
		{
			return scale;
		}
		public void resetScale()
		{
			this.scale = 1;
		}
		public void Reset()
		{
			//Sets all the values back to their original state.
			RecipeName = "";
			IngredientCount = 0;
			StepCount = 0;
			ingredients = null;
			recipeSteps = null;
			resetScale();
		}
		public string displayRecipe()
		{
			StringBuilder fullRecipe = new StringBuilder();
			fullRecipe.AppendLine("\n----" + this.RecipeName + " Recipe----");
			fullRecipe.AppendLine("\n----Ingredients----");
			for (int step = 0; step < IngredientCount; step++)
			{
				fullRecipe.AppendLine(this.ingredients[step].Name + " " + (this.ingredients[step].Amount * scale) + " " + this.ingredients[step].Measurment);
			}
			fullRecipe.AppendLine("\n\n----Recipe Steps----");
			for (int step = 0; step < StepCount; step++)
			{
				fullRecipe.AppendLine(this.recipeSteps[step]);
			}
			return fullRecipe.ToString();
		}
	}
}
