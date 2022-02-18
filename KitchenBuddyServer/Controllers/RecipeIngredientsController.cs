using KitchenBuddyServer.DataObjects;
using KitchenBuddyServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenBuddyServer.Controllers
{
    [ApiController]
    [Route("Recipes")]
    public class RecipeIngredientsController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext _db;

        /// <summary>
        /// Constructor to set the database context
        /// </summary>
        /// <param name="db"></param>
        public RecipeIngredientsController(DatabaseContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets a recipe ingredient by recipe id
        /// </summary>
        /// <param name="id">The id of the recipe to retrieve the ingredients</param>
        /// <returns>The recipe ingredients with that recipe id</returns>
        [HttpGet("{recipeId}/Ingredients")]
        public virtual IActionResult GetAllRecipeIngredients(int recipeId)
        {
            // If there's no recipe with the matching number, return a 404
            if (_db.Recipes.Find(recipeId) == null)
            {
                return NotFound();
            }

            // Get all the ingredients for the recipeId
            List<RecipeIngredient> ingredients = _db.RecipeIngredients.Where(
                ingredient => ingredient.RecipeId == recipeId).ToList();

            // Don't need to return a 404 if there's no ingredients, just return empty list

            // Return 200 with the recipe ingredients
            return Ok(ingredients);
        }

        /// <summary>
        /// Gets a recipe ingredient based on the recipe id and ingredient id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        [HttpGet("{recipeId}/Ingredients/{ingredientId}")]
        public virtual IActionResult GetRecipeIngredient(int recipeId, int ingredientId)
        {
            // If there's no ingredient with recipeId and ingredientId return 404
            RecipeIngredient? ingredient = _db.RecipeIngredients.Find(recipeId, ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }

            // Return a 200 with the ingredient
            return Ok(ingredient);
        }

        /// <summary>
        /// Create a new recipe ingredient
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPost("{recipeId}/Ingredients")]
        public virtual IActionResult AddRecipeIngredient(Recipe recipe)
        {
            // Add the recipe to the database and save the changes
            _db.Recipes.Add(recipe);
            _db.SaveChanges();

            // Return the newly created recipe with its id
            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        /// <summary>
        /// Update an existing recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPut("{recipeId}/Ingredients")]
        public virtual IActionResult UpdateRecipe(Recipe recipe)
        {
            // First check if there's no existing recipe with the same Id
            if (_db.Recipes.Find(recipe.RecipeId) == null)
            {
                // Return a 404
                return NotFound();
            }

            // Otherwise, update the recipe and save the changes
            _db.Recipes.Update(recipe);
            _db.SaveChanges();

            // Return a 200 with the recipe (could return a 204???)
            return Ok(recipe);
        }

        /// <summary>
        /// Delete an recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpDelete("{recipeId}/Ingredients")]
        public virtual IActionResult DeleteRecipe(RecipeIngredient recipe)
        {
            // Delete the recipe and save the changes
            //_db.Recipes.Remove(recipe);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
