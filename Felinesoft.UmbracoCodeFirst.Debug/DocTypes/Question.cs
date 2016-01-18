using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

using at = Felinesoft.UmbracoCodeFirst.Attributes;
using dt = Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	[DocumentType]
	public class Question : DocumentTypeBase
	{
		[ContentProperty(mandatory: true)]
		public dt.Textstring QuestionText { get; set; }

		[ContentProperty(mandatory: true)]
		[at.InstancePreValue("minItems", "4")]
		[at.InstancePreValue("maxItems", "4")]
		public dt.MultipleTextstring Answers { get; set; }

		[ContentProperty(mandatory: true)]
		public QuestionImage Image { get; set; }

		[ContentProperty(mandatory: true)]
		[at.InstancePreValue("max", "4")]
		[at.InstancePreValue("min", "1")]
		public dt.Numeric CorrectAnswer { get; set; }
	}

}