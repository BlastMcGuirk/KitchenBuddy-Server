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
        public string Name { get; set; }

        /// <summary>
        /// Description for the recipe
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The instructions for the recipe
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// The prep time in minutes
        /// </summary>
        public int PrepTime { get; set; }

        /// <summary>
        /// The cook time in minutes
        /// </summary>
        public int CookTime { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Recipe constructor
        /// </summary>
        public Recipe()
        {
            Name = "";
            Description = "";
            Instructions = "";
        }

        #endregion
    }
}
