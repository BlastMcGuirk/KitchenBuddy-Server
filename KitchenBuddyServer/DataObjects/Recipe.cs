namespace KitchenBuddyServer.DataObjects
{
    /// <summary>
    /// Represents the basic details of a recipe
    /// </summary>
    public class Recipe
    {
        #region Public Members

        /// <summary>
        /// The id of the recipe
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// The name of the recipe
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The instructions for the recipe
        /// </summary>
        public string? Instructions { get; set; }

        #endregion
    }
}
