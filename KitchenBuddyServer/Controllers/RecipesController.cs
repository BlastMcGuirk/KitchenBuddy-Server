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

            // Return 200 with the recipe
            return Ok(recipe);
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
        [HttpDelete]
        public virtual IActionResult DeleteRecipe(Recipe recipe)
        {
            // Delete the recipe and save the changes
            _db.Recipes.Remove(recipe);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
