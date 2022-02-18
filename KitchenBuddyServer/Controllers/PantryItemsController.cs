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
                            Id = i.Id,
                            Name = i.Name,
                            Units = i.Units,
                            Quantity = p.Quantity
                        }).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a pantry item by id
        /// </summary>
        /// <param name="id">The id of the pantry item to retrieve</param>
        /// <returns>The pantry item with that id</returns>
        [HttpGet("{id}/Pantry")]
        public virtual IActionResult GetPantryItem(int id)
        {
            var item = (from i in _db.Items
                        join p in _db.PantryItems
                        on i.Id equals p.ItemId
                        where i.Id == id
                        select new
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Units = i.Units,
                            Quantity = p.Quantity
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
            // Add the pantry item to the database and save the changes
            _db.PantryItems.Add(item);
            _db.SaveChanges();

            // Return the newly created pantry item with its id
            return CreatedAtAction("GetPantryItem", new { id = item.ItemId }, item);
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
            if (_db.PantryItems.Find(item.ItemId) == null)
            {
                // Return a 404
                return NotFound();
            }

            // Otherwise, update the pantry item and save the changes
            _db.PantryItems.Update(item);
            _db.SaveChanges();

            // Return a 200 with the pantry item (could return a 204???)
            return Ok(item);
        }

        /// <summary>
        /// Delete an pantry item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpDelete("Pantry")]
        public virtual IActionResult DeletePantryItem(PantryItem item)
        {
            // Delete the pantry item and save the changes
            _db.PantryItems.Remove(item);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
