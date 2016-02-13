using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.QuizDemo.MediaTypes;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes
{
	[DocumentType(allowAtRoot: false)]
	public class Factoid : DocumentTypeBase
	{
		public class FactoidDetailsTab : TabBase
		{
			[ContentProperty(sortOrder: 1)]
			public virtual Textstring FactoidTitle { get; set; }

			[ContentProperty(sortOrder: 2)]
			public virtual Textstring FactoidText { get; set; }

			[MediaPickerProperty(sortOrder: 3)]
			[NodePickerConfig(maximumItems: 1)]
			public virtual FactoidImageMedia Image { get; set; }
		}

		[ContentTab]
		public FactoidDetailsTab Details { get; set; }
	}
}