using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using at = Felinesoft.UmbracoCodeFirst.Attributes;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Models;
using Felinesoft.UmbracoCodeFirst.Controllers;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	[at.DocumentType(enableListView: true)]
	[at.EventHandler(typeof(QuestionSetSurfaceController))]
	[at.Template(true)]
	public class QuestionSet : ListViewDocumentType<Question>
	{
		public QuestionSet()
		{
			Messages = new MessageTab();
        }

		[at.ContentProperty]
		public RichtextEditor WelcomeParagraph { get; set; }

		public class MessageTab : TabBase
		{
			[at.ContentProperty]
			public Textstring QuestionVersionChangedErrorMessage { get; set; }

			[at.ContentProperty]
			public Textstring SetVersionChangedErrorMessage { get; set; }

			[at.ContentProperty]
			public Textstring IncorrectAnswerMessage { get; set; }

			[at.ContentProperty]
			public Textstring CorrectAnswerMessage { get; set; }

			[at.ContentProperty]
			public Textstring NoMoreQuestionsMessage { get; set; }
		}

		[at.ContentTab]
		public MessageTab Messages { get; set; }
	}

}