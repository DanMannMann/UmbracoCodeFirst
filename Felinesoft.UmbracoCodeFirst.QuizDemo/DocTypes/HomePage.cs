using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.Linq;
using Marsman.UmbracoCodeFirst.Attributes;

namespace Marsman.UmbracoCodeFirst.QuizDemo.DocTypes
{
	[DocumentType(allowedChildren: new Type[] { typeof(QuestionSet) }, allowAtRoot: true)]
	[Template(true)]
	public class HomePage : DocumentTypeBase
	{
		public class ContentTab : TabBase
		{
			[ContentProperty]
			public virtual RichtextEditor WelcomeParagraph { get; set; }
		}

		[ContentTab]
		public ContentTab Content { get; set; }

		/// <summary>
		/// A calculated property which looks up the question sets which
		/// are children of this node.
		/// </summary>
		public IEnumerable<QuestionSet> QuestionSets
		{
			get
			{
				return this.ChildrenOfType<QuestionSet>();
			}
		}
	}
}