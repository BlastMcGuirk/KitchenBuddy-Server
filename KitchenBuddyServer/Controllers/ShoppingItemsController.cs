using KitchenBuddyServer.DataObjects;
using KitchenBuddyServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenBuddyServer.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ShoppingItemsController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext _db;

        /// <summary>
        /// Constructor to set the database context
        /// </summary>
        /// <param name="db"></param>
        public ShoppingItemsController(DatabaseContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all of the shopping items in the database
        /// </summary>
        /// <returns>All of the items in the database</returns>
        [HttpGet("Shopping")]
        public virtual IActionResult GetAll() => Ok(_db.ShoppingItems.ToList());

        /// <summary>
        /// Gets a shopping item by id
        /// </summary>
        /// <param name="id">The id of the shopping item to retrieve</param>
        /// <returns>The shopping item with that id</returns>
        [HttpGet("{id}/Shopping")]
        public virtual IActionResult GetShoppingItem(int id)
        {
            // Look for the shopping item by Id
            ShoppingItem? item = _db.ShoppingItems.Find(id);

            // Return 404 if the shopping item doesn't exist
            if (item == null)
            {
                return NotFound();
            }

            // Return 200 with the shopping item
            return Ok(item);
        }

        /// <summary>
        /// Create a new shopping item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("Shopping")]
        public virtual IActionResult AddShoppingItem(ShoppingItem item)
        {
            // Add the shopping item to the database and save the changes
            _db.ShoppingItems.Add(item);
            _db.SaveChanges();

            // Return the newly created shopping item with its id
            return CreatedAtAction("GetShoppingItem", new { id = item.ItemId }, item);
        }

        /// <summary>
        /// Update an existing shopping item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("Shopping")]
        public virtual IActionResult UpdateShoppingItem(ShoppingItem item)
        {
            // First check if there's no existing shopping item with the same Id
            if (_db.ShoppingItems.Find(item.ItemId) == null)
            {
                // Return a 404
                return NotFound();
            }

            // Otherwise, update the shopping item and save the changes
            _db.ShoppingItems.Update(item);
            _db.SaveChanges();

            // Return a 200 with the shopping item (could return a 204???)
            return Ok(item);
        }

        /// <summary>
        /// Delete an shopping item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpDelete("Shopping")]
        public virtual IActionResult DeleteShoppingItem(ShoppingItem item)
        {
            // Delete the shopping item and save the changes
            _db.ShoppingItems.Remove(item);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
