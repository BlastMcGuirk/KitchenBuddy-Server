using System.ComponentModel.DataAnnotations;

namespace KitchenBuddyServer.DataObjects
{
    /// <summary>
    /// Represents the basic details of an item
    /// </summary>
    public class Item
    {
        #region Public Members

        /// <summary>
        /// The id of the item
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The units for the item
        /// </summary>
        public string Units { get; set; }

        #endregion

        #region Constructor

        public Item()
        {
            Name = "";
            Units = "";
        }

        #endregion
    }
}
