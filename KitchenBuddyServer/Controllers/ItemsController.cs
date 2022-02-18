using KitchenBuddyServer.DataObjects;
using KitchenBuddyServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenBuddyServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext _db;

        /// <summary>
        /// Constructor to set the database context
        /// </summary>
        /// <param name="db"></param>
        public ItemsController(DatabaseContext db)
        {
            _db = db;
        }
        
        /// <summary>
        /// Gets all of the items in the database
        /// </summary>
        /// <param name="q">Optional search filter term</param>
        /// <returns>All of the items in the database (that contain query q)</returns>
        [HttpGet]
        public virtual IActionResult GetAll([FromQuery]string? q)
        {
            // If there's no query, return all the items
            if (q == null)
            {
                return Ok(_db.Items.ToList());
            }

            // Search for all items that contain the search query
            List<Item> items = _db.Items.Where(item => 
                item.Name.ToLower().Contains(q.ToLower())
            ).ToList();

            // Return the items
            return Ok(items);
        }

        /// <summary>
        /// Gets an item by id
        /// </summary>
        /// <param name="id">The id of the item to retrieve</param>
        /// <returns>The item with that id</returns>
        [HttpGet("{id}")]
        public virtual IActionResult GetItem(int id)
        {
            // Look for the item by Id
            Item? item = _db.Items.Find(id);

            // Return 404 if the item doesn't exist
            if (item == null)
            {
                return NotFound();
            }

            // Return 200 with the item if it does
            return Ok(item);
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult AddItem(Item item)
        {
            // Add the item to the database and save the changes
            _db.Items.Add(item);
            _db.SaveChanges();

            // Return the newly created item with its id
            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual IActionResult UpdateItem(Item item)
        {
            // First check if there's no existing item with the same Id
            if (_db.Items.Find(item.Id) == null)
            {
                // Return a 404
                return NotFound();
            }

            // Otherwise, update the item and save the changes
            _db.Items.Update(item);
            _db.SaveChanges();

            // Return a 200 with the item (could return a 204???)
            return Ok(item);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual IActionResult DeleteItem(Item item)
        {
            // Delete the item and save the changes
            _db.Items.Remove(item);
            _db.SaveChanges();

            // Return a 204
            return NoContent();
        }
    }
}
