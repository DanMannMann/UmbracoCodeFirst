using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	[DataType(propertyEditorAlias: "RJP.MultiUrlPicker")]
	public class RjpUrl : UmbracoJsonCollectionDataType<UrlLink>
	{
		
	}
}