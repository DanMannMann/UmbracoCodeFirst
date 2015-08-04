using System;
using Felinesoft.UmbracoCodeFirst.Core;
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
using Felinesoft.UmbracoCodeFirst.Core.Modules;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
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
        private void Init()
        {
            if (_preValues == null)
            {
                _preValues = CodeFirstModelContext.GetContext(this).CurrentPreValues;
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
                Init();
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
                Init();
                return _preValues;
            }
            protected set
            {
                _preValues = value;
            }
        }
    }
}
