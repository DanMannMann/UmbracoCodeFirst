using System;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Core.Modules;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    /// <summary>
    /// A base class for data types which select multiple values from a list of prevalues
    /// </summary>
    public abstract class MultiselectDataType : SelectListDataType, IUmbracoNtextDataType
    {
        private ReadOnlyCollection<string> _invalidSelections;

        /// <summary>
        /// Gets the selected values
        /// </summary>
        public IEnumerable<string> SelectedValues
        {
            get
            {
                return Items.Where(x => x.Selected).Select(x => x.Value);
            }
        }

        /// <summary>
        /// Gets any selected values which are no longer in the collection of valid options
        /// </summary>
        public IEnumerable<string> InvalidSelections
        {
            get
            {
                return _invalidSelections;
            }
        }

        /// <summary>
        /// Gets all of the items
        /// </summary>
        public ReadOnlyCollection<Item> Items { get; private set; }

        /// <summary>
        /// Gets an item by its index
        /// </summary>
        public Item this[int index]
        {
            get
            {
                return Items[index];
            }
        }

        /// <summary>
        /// Gets an item by its value
        /// </summary>
        public Item this[string value]
        {
            get
            {
                return Items.FirstOrDefault(x => x.Value == value);
            }
        }

        /// <summary>
        /// Initialises the instance from a comma-separated list of values
        /// </summary>
        /// <param name="dbValue">a comma-separated list of values</param>
        public void Initialise(string dbValue)
        {
            var selectedValues = dbValue.Split(',');
            Items = Options.Select(x => new Item(x, selectedValues.Contains(x))).ToList().AsReadOnly();
            _invalidSelections = selectedValues.Where(x => !Options.Contains(x)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Serialises the instance to a comma-separated list of values
        /// </summary>
        /// <returns>a comma-separated list of values</returns>
        public string Serialise()
        {
            string serial = "";
            foreach (var value in Items.Where(x => x.Selected).Select(x => x.Value))
            {
                if (serial.Length > 0)
                {
                    serial += ",";
                }
                serial += value;
            }
            return serial;
        }

        public override string ToString()
        {
            return Serialise();
        }
    }
}
