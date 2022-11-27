using KitchenBuddyServer.DataObjects;
using KitchenBuddyServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenBuddyServer.Controllers
{
    [ApiController]
    [Route("Items")]
    public class PantryItemsController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext _db;

        /// <summary>
        /// Constructor to set the database context
        /// </summary>
        /// <param name="db"></param>
        public PantryItemsController(DatabaseContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all of the pantry items in the database
        /// </summary>
        /// <returns>All of the items in the database</returns>
        [HttpGet("Pantry")]
        public virtual IActionResult GetAll()
        {
            // Join the pantry items with items
            var items = (from i in _db.Items
                        join p in _db.PantryItems
                        on i.Id equals p.ItemId
                        select new
                        {
                            i.Id,
                            i.Name,
                            i.Units,
                            p.Quantity,
                            p.Expiration
                        }).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a pantry item by id
        /// </summary>
        /// <param name="id">The id of the pantry item to retrieve</param>
        /// <returns>The pantry item with that id</returns>
        [HttpGet("Pantry/{id}")]
        public virtual IActionResult GetPantryItem(int id)
        {
            var item = (from i in _db.Items
                        join p in _db.PantryItems
                        on i.Id equals p.ItemId
                        where i.Id == id
                        select new
                        {
                            i.Id,
                            i.Name,
                            i.Units,
                            p.Quantity,
                            p.Expiration
                        }).FirstOrDefault();

            // Return 404 if the pantry item doesn't exist
            if (item == null)
            {
                return NotFound();
            }

            // Return 200 with the pantry item
            return Ok(item);
        }

        /// <summary>
        /// Create a new pantry item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("Pantry")]
        public virtual IActionResult AddPantryItem(PantryItem item)
        {
            // Check if an existing pantry item exists
            PantryItem? existing = _db.PantryItems.FirstOrDefault(pi => pi.ItemId == item.ItemId);
            if (existing != null)
            {
                return BadRequest($"Pantry item for item {item.ItemId} already exists");
            }

            // Add the pantry item to the database and save the changes
            _db.PantryItems.Add(item);
            _db.SaveChanges();

            // Return the newly created pantry item with its id
            return CreatedAtAction(nameof(GetPantryItem), new { id = item.ItemId }, item);
        }

        /// <summary>
        /// Update an existing pantry item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("Pantry")]
        public virtual IActionResult UpdatePantryItem(PantryItem item)
        {
            // First check if there's no existing pantry item with the same Id
            PantryItem? existingItem = _db.PantryItems
                .FirstOrDefault(pantryItem => pantryItem.ItemId == item.ItemId);
            if (existingItem == null)
            {
                // Return a 404
                return NotFound();
            }

            // Update the item
            existingItem.Quantity = item.Quantity;
            existingItem.Expiration = item.Expiration;

            // Otherwise, update the pantry item and save the changes
            _db.PantryItems.Update(existingItem);
            _db.SaveChanges();

            // Return a 200 with the pantry item (could return a 204???)
            return Ok(existingItem);
        }

        /// <summary>
        /// Delete an pantry item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpDelete("Pantry/{id}")]
        public virtual IActionResult DeletePantryItem(int id)
        {
            // First check if there's an existing pantry item with the same Id
            PantryItem? existingItem = _db.PantryItems
                .FirstOrDefault(pantryItem => pantryItem.ItemId == id);
            if (existingItem == null)
            {
                // Return a 404
                return NotFound();
            }

            // Delete the pantry item and save the changes
            _db.PantryItems.Remove(existingItem);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
