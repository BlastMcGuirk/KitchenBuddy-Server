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

        #endregion
    }
}
