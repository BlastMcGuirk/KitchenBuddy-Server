using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenBuddyServer.DataObjects
{
    /// <summary>
    /// Represents an item in the pantry
    /// </summary>
    public class PantryItem
    {
        #region Public Members
        
        /// <summary>
        /// The id of the pantry item
        /// </summary>
        public int PantryItemId { get; set; }

        /// <summary>
        /// The id of the item it references
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the item in the pantry
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The expiration date for the pantry item
        /// </summary>
        [Column(TypeName="Date")]
        public DateTime? Expiration { get; set; }

        #endregion
    }
}
