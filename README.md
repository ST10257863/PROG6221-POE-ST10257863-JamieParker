# Project Update: Implemented Feedback Changes

In response to the feedback, several critical improvements have been made to the project. Firstly, the application has been updated to target .NET Framework 4.8 and necessary references have been added. Error handling has been enhanced to manage null values, incorrect value types, and to display appropriate error messages to the user. The layout has been improved by adding sufficient spacing between input and output sections, and incorporating lines to better organize the UI. Advanced features, such as coloured text in the display, have been utilized to enhance user experience. A reset function has been implemented to revert scaled recipes back to their original values, and users are prompted to confirm before clearing any data. To adhere to coding standards, separator lines have been added between methods, an end-of-file line has been included, comments have been made more meaningful, and classes have been split into separate files for better organization.

# Recipe Management System

## Overview
This project is a console-based recipe management system developed in C# using the .NET Framework 4.8. The application allows users to create, manage, and scale recipes while adhering to specific coding standards and implementing robust error handling.

## Features
- **Create Recipes**: Users can create new recipes by entering a recipe name, ingredients, and preparation steps.
- **Select and Edit Recipes**: Users can select a recipe from a list, view its details, scale the ingredients, reset the scaling, and delete recipes.
- **Display All Recipes**: Users can view all created recipes.
- **Calorie Count Notification**: The system notifies users if the total calorie count exceeds a specified limit.

## Installation
1. Ensure you have .NET Framework 4.8 installed.
2. Clone the repository:
   ```sh
   git clone https://https://github.com/ST10257863/PROG6221-POE-ST10257863-JamieParker
   ```
3. Open the solution in Visual Studio.
4. Restore the NuGet packages.
5. Build and run the application.

## Usage
1. **Main Menu**: 
   - Create new recipe
   - Select recipe
   - Display all recipes
   - Exit

2. **Creating a Recipe**: 
   - Enter the recipe name
   - Add ingredients (name, amount, measurement, calories, food group)
   - Add preparation steps

3. **Editing a Recipe**:
   - Display recipe details
   - Scale recipe by a factor
   - Reset recipe to original values
   - Delete recipe

## Advanced Features
- **Coloured Text**: Error messages and important prompts are displayed in different colours for better visibility.
- **Error Handling**: The application includes error handling for null values, incorrect value types, and displays appropriate error messages.
- **User Confirmation**: Users are asked to confirm actions such as deleting a recipe.

## Code Structure
- **Classes**: Defined in separate files under the `PROG6221_POE_ST10257863_JamieParker.Classes` namespace.
- **Methods**: Clear separation between methods, with meaningful comments and end-of-file lines.

## Coding Standards
- Separator lines between methods.
- End-of-file line in all files.
- Meaningful comments for better code readability.
- Classes split into separate files for modularity.

## Testing
Unit tests are implemented using MSTest to ensure the correctness of recipe creation, ingredient addition, step addition, and resetting functionality.

## Contributing
Feel free to fork the repository and submit pull requests. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License.

## Refrences
Troelsen, A. and Japikse, P. (2022) Pro C# 10 with .NET 6: Foundational Principles and Practices in Programming. 11th edn. Cham, Switzerland: Apress. Available at: https://doi.org/10.1007/978-1-4842-7869-7 (Accessed: 30 May 2024).

Microsoft (2024) .NET documentation. Available at: https://learn.microsoft.com/en-us/dotnet/ (Accessed: 30 May 2024).
