using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Core.Models;
using Umbraco.Core;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// A base class for data types which select values from a list of prevalues
    /// </summary>
    public abstract class SelectListDataType
    {
        private List<string> _options;
        private IEnumerable<PreValue> _preValues;

        /// <summary>
        /// Initialises the prevalues and options collections
        /// </summary>
        private void init()
        {
            if (_preValues == null)
            {
                Type type = this.GetType();
                if (DataTypeRegister.Current.IsRegistered(type))
                {
                    _preValues = PreValueCache.Get(this.GetType());
                }
                else
                {
                    _preValues = new List<PreValue>();
                }
            }

            if (_options == null)
            {
                _options = _preValues.Select(x => x.Value).ToList();
            }
        }

        /// <summary>
        /// Gets the list of valid options for this data type
        /// </summary>
        public virtual List<string> Options
        {
            get
            {
                init();
                return _options;
            }
            protected set
            {
                _options = value;
            }
        }

        /// <summary>
        /// Gets the prevalues for this data type
        /// </summary>
        public virtual IEnumerable<PreValue> PreValues
        {
            get
            {
                init();
                return _preValues;
            }
            protected set
            {
                _preValues = value;
            }
        }
    }
}
