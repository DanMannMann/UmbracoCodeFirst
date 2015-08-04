using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in dropdown list data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.Dropdown, propertyEditorAlias: BuiltInPropertyEditorAliases.DropDown)]
    [DoNotSyncDataType][BuiltInDataType]
    public class DropdownList : SingleSelectDataType, IUmbracoNtextDataType
    {
        public void Initialise(string dbValue)
        {
            base.Initialise(dbValue);
        }
    }
}
