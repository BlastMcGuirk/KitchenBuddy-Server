using System.ComponentModel.DataAnnotations;

namespace KitchenBuddyServer.DataObjects
{
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

        /// <summary>
        /// The units of the item needed
        /// </summary>
        public string Units { get; set; }

        #endregion

        #region Constructor

        public RecipeIngredient()
        {
            Units = "";
        }

        #endregion
    }
}
