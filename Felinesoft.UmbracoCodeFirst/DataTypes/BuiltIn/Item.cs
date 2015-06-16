using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// An item from a list of valid options, including a flag indicating whether the item is selected
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The value of the item
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// True if the item is selected
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// An item from a list of valid options, including a flag indicating whether the item is selected
        /// </summary>
        /// <param name="value">The value of the item</param>
        /// <param name="isSelected">True if the item is selected</param>
        public Item(string value, bool isSelected)
        {
            Value = value;
            Selected = isSelected;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
