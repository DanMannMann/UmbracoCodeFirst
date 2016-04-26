using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.QuizDemo.MediaTypes;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes
{
	[DocumentType(allowAtRoot: false)]
	[EventHandler]
	public class Factoid : DocumentTypeBase, Events.IOnLoad<Factoid>
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

		public void OnLoad(Factoid model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage)
		{
			
		}
	}
}