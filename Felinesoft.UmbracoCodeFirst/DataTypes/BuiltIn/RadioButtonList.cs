using Felinesoft.UmbracoCodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Drawing;
using System.Collections.ObjectModel;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in radio button group data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.Radiobox, propertyEditorAlias: BuiltInPropertyEditorAliases.RadioButtonList)]
    [BuiltInDataType]
    public class RadioButtonList : SingleSelectDataType, IUmbracoIntDataType
    {
        /// <summary>
        /// Initialises the instance from the Umbraco prevalue ID
        /// </summary>
        /// <param name="dbValue">the Umbraco prevalue ID</param>
        public void Initialise(int dbValue)
        {
            if (PreValues.FirstOrDefault(x => x.Id == dbValue) == null)
            {
                throw new CodeFirstException("Invalid prevalue ID");
            }
            base.Initialise(PreValues.First(x => x.Id == dbValue).Value);
        }

        /// <summary>
        /// Returns the Umbraco PreValue ID of the selected item, or -1 if an invalid selection is made
        /// </summary>
        /// <returns>the Umbraco prevalue ID</returns>
        public new int Serialise()
        {
            var preVal = PreValues.FirstOrDefault(x => x.Value == Options[SelectedIndex]);
            if (preVal == null)
            {
                return -1;
            }
            else
            {
                return preVal.Id;
            }
        }
    }
}