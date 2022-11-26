namespace KitchenBuddyServer.DataObjects
{
    /// <summary>
    /// Represents the basic details of a recipe ingredient
    /// </summary>
    public class RecipeIngredient
    {
        #region Public Members

        /// <summary>
        /// The id of the recipe
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// The id of the item
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the item needed
        /// </summary>
        public int Quantity { get; set; }

        #endregion
    }
}
