using System.ComponentModel.DataAnnotations;

namespace KitchenBuddyServer.DataObjects
{
    /// <summary>
    /// Represents an item in the pantry
    /// </summary>
    public class PantryItem
    {
        #region Public Members

        /// <summary>
        /// The id of the item
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the item in the pantry
        /// </summary>
        public int Quantity { get; set; }

        #endregion
    }
}
