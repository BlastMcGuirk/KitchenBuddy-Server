using System.ComponentModel.DataAnnotations;

namespace KitchenBuddyServer.DataObjects
{
    /// <summary>
    /// Represents an item in the shopping cart
    /// </summary>
    public class ShoppingItem
    {
        #region Public Members

        /// <summary>
        /// The id of the item
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the item in the shopping cart
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Whether or not the shopping item is checked off
        /// </summary>
        public bool IsChecked { get; set; }

        #endregion
    }
}
