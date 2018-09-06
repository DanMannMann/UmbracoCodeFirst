
using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Marsman.UmbracoCodeFirst.Core;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.DateTime", "Date Picker with time")]
    [DoNotSyncDataType][BuiltInDataType]
    public class DatePickerWithTime : DatePicker
    {
		public static implicit operator DatePickerWithTime(DateTime dateTime)
		{
			return new DatePickerWithTime() { Value = dateTime };
		}

	}
}