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
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet("{recipeId}/Ingredients/{itemId}")]
        public virtual IActionResult GetRecipeIngredient(int recipeId, int itemId)
        {
            // If there's no ingredient with recipeId and itemId return 404
            RecipeIngredient? ingredient = _db.RecipeIngredients
                .FirstOrDefault(ri => ri.RecipeId == recipeId &&
                                        ri.ItemId == itemId);
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
        /// <param name="recipeIngredient"></param>
        /// <returns></returns>
        [HttpPost("{recipeId}/Ingredients")]
        public virtual IActionResult AddRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            // Add the recipe ingredient to the database and save the changes
            _db.RecipeIngredients.Add(recipeIngredient);
            _db.SaveChanges();

            // Return the newly created recipe ingredient with its id
            return CreatedAtAction("GetRecipeIngredient",
                new { recipeId = recipeIngredient.RecipeId, itemId = recipeIngredient.ItemId },
                recipeIngredient);
        }

        /// <summary>
        /// Update an existing recipe
        /// </summary>
        /// <param name="recipeIngredient"></param>
        /// <returns></returns>
        [HttpPut("{recipeId}/Ingredients")]
        public virtual IActionResult UpdateRecipe(RecipeIngredient recipeIngredient)
        {
            // First check if there's an existing recipe ingredient with the same Ids
            RecipeIngredient? existing = _db.RecipeIngredients
                .FirstOrDefault(ri => ri.RecipeId == recipeIngredient.RecipeId &&
                                        ri.ItemId == recipeIngredient.ItemId);
            if (existing == null)
            {
                // Return a 404
                return NotFound();
            }

            // Update the existing item
            existing.Quantity = recipeIngredient.Quantity;

            // Otherwise, update the recipe and save the changes
            _db.RecipeIngredients.Update(existing);
            _db.SaveChanges();

            // Return a 200 with the recipe (could return a 204???)
            return Ok(existing);
        }

        /// <summary>
        /// Delete a recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpDelete("{recipeId}/Ingredients/{itemId}")]
        public virtual IActionResult DeleteRecipe(int recipeId, int itemId)
        {
            // First check if there's an existing recipe ingredient with the same Ids
            RecipeIngredient? existing = _db.RecipeIngredients
                .FirstOrDefault(ri => ri.RecipeId == recipeId &&
                                        ri.ItemId == itemId);
            if (existing == null)
            {
                // Return a 404
                return NotFound();
            }

            // Delete the recipe and save the changes
            _db.RecipeIngredients.Remove(existing);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
