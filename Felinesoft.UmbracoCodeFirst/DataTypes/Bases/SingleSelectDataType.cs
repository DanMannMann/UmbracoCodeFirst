using Marsman.UmbracoCodeFirst;
using System;
using Marsman.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Web;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Drawing;
using Marsman.UmbracoCodeFirst.Core.Modules;

namespace Marsman.UmbracoCodeFirst.DataTypes
{
    /// <summary>
    /// A base class for data types which select a single value from a list of prevalues
    /// </summary>
    /// <remarks>
    /// This works with editors which store the actual string value as the property value.
    /// Editors which store the prevalue ID are not supported and in this case one should override 
    /// Serialise and Initialise in a derived class to get the actual value based on the ID.
    /// </remarks>
    public abstract class SingleSelectDataType : SelectListDataType
    {
        private string _selectedValue;

        /// <summary>
        /// Returns true if the selected value is contained in the current list of prevalue options
        /// </summary>
        public bool SelectedIsValid
        {
            get
            {
                return Options.Contains(_selectedValue);
            }
        }

        /// <summary>
        /// Returns the index of the currently selected item, or -1 if the selected value is no longer valid.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (SelectedIsValid)
                {
                    return Options.IndexOf(_selectedValue);
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (value > Options.Count || value < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                _selectedValue = Options[value];
            }
        }

        /// <summary>
        /// Gets and sets the selected value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the input value is not in the Options collection</exception>
        public string SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                if (!Options.Contains(value))
                {
                    throw new InvalidOperationException(value + " is not in the list of valid options");
                }
                _selectedValue = value;
            }
        }

        /// <summary>
        /// Allows an invalid value to be explicitly set, bypassing
        /// the protection in the SelectedValue property setter.
        /// </summary>
        /// <param name="value">The value to set</param>
        /// <remarks>This is equivalent to calling the base implementation of Initialise but, as Initialise is virtual, this 
        /// method should be preferred as it guarantees the correct behaviour in derived types</remarks>
        protected void SetInvalidValue(string value)
        {
            _selectedValue = value;
        }

        /// <summary>
        /// Initialises the current instance from a string containing a single value
        /// </summary>
        /// <param name="value">The serialised value</param>
        public virtual void Initialise(string value)
        {
            SetInvalidValue(value); //ensure the value gets set, regardless of if it is in the collection of options
        }

        /// <summary>
        /// Serialises the current instance to a string containing the selected value
        /// </summary>
        /// <returns>The serialised instance</returns>
        public virtual string Serialise()
        {
            return _selectedValue;
        }

        public override string ToString()
        {
            return _selectedValue;
        }
    }
}