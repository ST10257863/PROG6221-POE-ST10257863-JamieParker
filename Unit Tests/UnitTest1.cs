using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG6221_POE_ST10257863_JamieParker.Classes;

namespace Unit_Tests
{
	[TestClass]
	public class RecipeTests
	{
		[TestMethod]
		public void AddIngredient_AddsIngredientToRecipe()
		{
			// Arrange
			var recipe = new Recipe();
			var ingredient = new Ingredient("Milk", 1, "cup", 100, "Dairy");

			// Act
			recipe.AddIngredient(ingredient);

			// Assert
			Assert.AreEqual(1, recipe.Ingredients.Count);
			var addedIngredient = recipe.Ingredients[0];
			Assert.AreEqual("Milk", addedIngredient.Name);
			Assert.AreEqual(1, addedIngredient.Amount);
			Assert.AreEqual("cup", addedIngredient.Measurement);
			Assert.AreEqual(100, addedIngredient.Calories);
			Assert.AreEqual("Dairy", addedIngredient.FoodGroup);
		}

		[TestMethod]
		public void AddRecipeStep_AddsStepToRecipe()
		{
			// Arrange
			var recipe = new Recipe();
			var step = "Mix ingredients";

			// Act
			recipe.AddRecipeStep(step);

			// Assert
			Assert.AreEqual(1, recipe.RecipeSteps.Count);
			var addedStep = recipe.RecipeSteps[0];
			Assert.AreEqual("Mix ingredients", addedStep);
		}

		[TestMethod]
		public void Reset_ResetsRecipeProperties()
		{
			// Arrange
			var recipe = new Recipe();
			var ingredient = new Ingredient("Milk", 1, "cup", 100, "Dairy");
			recipe.AddIngredient(ingredient);
			recipe.RecipeName = "Test Recipe";
			recipe.AddRecipeStep("Test Step");
			recipe.setScale(2);

			// Act
			recipe.Reset();

			// Assert
			Assert.AreEqual("", recipe.RecipeName);
			Assert.AreEqual(0, recipe.Ingredients.Count);
			Assert.AreEqual(0, recipe.RecipeSteps.Count);
			Assert.AreEqual(1, recipe.getScale());
			Assert.AreEqual(0, recipe.TotalCalories);
		}
	}
}