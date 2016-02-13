using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Events;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes
{
	//Using ListViewDocumentType<T> without enableListView = true results in ordinary back-office folder tree behaviour, but
	//with the allowed children still restricted to one type (Factoid in this case) and the document model (Factoids)
	//still implementing IEnumerable<Factoid> & IList<Factoid> over the children.
	[DocumentType(allowAtRoot: true, enableListView: false)]
	public class Factoids : ListViewDocumentType<Factoid>
	{

	}
}