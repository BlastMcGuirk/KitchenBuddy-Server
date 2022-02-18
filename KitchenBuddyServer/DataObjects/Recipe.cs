using System.ComponentModel.DataAnnotations;

namespace KitchenBuddyServer.DataObjects
{
    public class Recipe
    {
        #region Public Members

        /// <summary>
        /// The id of the recipe
        /// </summary>
        [Key]
        public int RecipeId { get; set; }

        /// <summary>
        /// The name of the recipe
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructor

        public Recipe()
        {
            Name = "";
        }

        #endregion
    }
}
