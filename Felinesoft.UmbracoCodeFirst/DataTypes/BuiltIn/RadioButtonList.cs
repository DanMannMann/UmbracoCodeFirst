using Felinesoft.UmbracoCodeFirst;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
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
    [DoNotSyncDataType][BuiltInDataType]
    public class RadioButtonList : SingleSelectDataType, IUmbracoIntegerDataType
    {
        /// <summary>
        /// Initialises the instance from the Umbraco prevalue ID
        /// </summary>
        /// <param name="dbValue">the Umbraco prevalue ID</param>
        public void Initialise(int dbValue)
        {
            var pval = PreValues.FirstOrDefault(x => x.Id == dbValue);
            if (pval == null)
            {
                base.Initialise(string.Empty);
            }
            else
            {
                base.Initialise(pval.Value);
            }
        }

        /// <summary>
        /// Returns the Umbraco PreValue ID of the selected item, or -1 if an invalid selection is made
        /// </summary>
        /// <returns>the Umbraco prevalue ID</returns>
        public int Serialise()
        {
            if (SelectedIndex == -1)
            {
                return 0;
            }

            var preVal = PreValues.FirstOrDefault(x => x.Value == Options[SelectedIndex]);
            if (preVal == null)
            {
                return 0;
            }
            else
            {
                return preVal.Id;
            }
        }
    }
}