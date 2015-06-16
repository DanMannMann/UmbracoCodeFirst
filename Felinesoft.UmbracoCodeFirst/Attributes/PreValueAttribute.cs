using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies a string prevalue to apply to a data type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PreValueAttribute : MultipleCodeFirstAttribute
    {
        /// <summary>
        /// Specifies a string prevalue to apply to a data type
        /// </summary>
        /// <param name="alias"> The alias of the prevalue</param>
        /// <param name="value"> The value of the prevalue</param>
        /// <param name="id"> The id of the prevalue</param>
        /// <param name="sortOrder"> The sort order of the prevalue</param>
        public PreValueAttribute(string alias, string value, int sortOrder, int id = 0)
        {
            Alias = alias;
            PreValue = new PreValue(id, value, sortOrder);
        }

        /// <summary>
        /// Specifies a string prevalue to apply to a data type
        /// </summary>
        /// <param name="alias"> The alias of the prevalue</param>
        /// <param name="value"> The value of the prevalue</param>
        public PreValueAttribute(string alias, string value)
        {
            Alias = alias;
            PreValue = new PreValue(value);
        }

        /// <summary>
        /// The alias of the prevalue
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// The prevalue instance
        /// </summary>
        public PreValue PreValue { get; private set; }
    }
}
