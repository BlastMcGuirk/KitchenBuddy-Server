using KitchenBuddyServer.DataObjects;
using KitchenBuddyServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenBuddyServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext _db;

        /// <summary>
        /// Constructor to set the database context
        /// </summary>
        /// <param name="db"></param>
        public RecipesController(DatabaseContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all of the recipes in the database
        /// </summary>
        /// <returns>All of the recipes in the database</returns>
        [HttpGet]
        public virtual IActionResult GetAll([FromQuery] string? q)
        {
            // If there's no query, return all the items
            if (q == null)
            {
                return Ok(_db.Recipes.ToList());
            }

            // Search for all items that contain the search query
            List<Recipe> recipes = _db.Recipes.Where(recipe => 
                recipe.Name.ToLower().Contains(q.ToLower())
            ).ToList();

            // Return a 200 with all the recipes
            return Ok(recipes);
        }

        /// <summary>
        /// Gets a recipe by id
        /// </summary>
        /// <param name="id">The id of the recipe to retrieve</param>
        /// <returns>The recipe with that id</returns>
        [HttpGet("{id}")]
        public virtual IActionResult GetRecipe(int id)
        {
            // Look for the recipe by Id
            Recipe? recipe = _db.Recipes.Find(id);

            // Return 404 if the recipe doesn't exist
            if (recipe == null)
            {
                return NotFound();
            }

            // Get the ingredients
            List<RecipeIngredient> ingredients = _db.RecipeIngredients.Where(ri =>
                ri.RecipeId == id).ToList();

            // Return 200 with the recipe
            return Ok(new
            {
                recipe.RecipeId,
                recipe.Name,
                recipe.Description,
                recipe.Instructions,
                recipe.PrepTime,
                recipe.CookTime,
                Ingredients = ingredients
            });
        }

        /// <summary>
        /// Create a new recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult AddRecipe(Recipe recipe)
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
        [HttpPut]
        public virtual IActionResult UpdateRecipe(Recipe recipe)
        {
            // First check if there's no existing recipe with the same Id
            Recipe? existing = _db.Recipes.Find(recipe.RecipeId);
            if (existing == null)
            {
                // Return a 404
                return NotFound();
            }

            // Update the recipe
            existing.Name = recipe.Name;
            existing.Description = recipe.Description;
            existing.Instructions = recipe.Instructions;
            existing.PrepTime = recipe.PrepTime;
            existing.CookTime = recipe.CookTime;

            // Otherwise, update the recipe and save the changes
            _db.Recipes.Update(existing);
            _db.SaveChanges();

            // Return a 200 with the recipe (could return a 204???)
            return Ok(existing);
        }

        /// <summary>
        /// Delete an recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual IActionResult DeleteRecipe(int id)
        {
            // First check for the recipe
            Recipe? recipe = _db.Recipes.Find(id);
            if (recipe == null)
            {
                // Return a 404
                return NotFound();
            }

            // Delete the recipe and save the changes
            _db.Recipes.Remove(recipe);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
