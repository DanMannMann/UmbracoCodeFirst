using System;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	public class QuestionSetViewModel
	{
		public Question CurrentQuestion(QuestionSet set)
		{
			return QuestionIndex >= 0 && QuestionIndex <= MaxIndex ? set.ElementAt(QuestionIndex) : null;
		}

		public int SetId { get; set; }

		public Guid SetVersion { get; set; }

		public int MaxIndex { get; set; }

		public QuestionResponse Answer { get; set; } = new QuestionResponse();

		public AnswerResponse Reply { get; set; } = new AnswerResponse();

		public int QuestionIndex { get; set; }

		public int QuestionId { get; set; }

		public Guid QuestionVersion { get; set; }

		public string RequiredPartial { get; set; } = "_question";

		[Bind(Prefix = "Answer")]
		public class QuestionResponse
		{
			public int AnswerIndex { get; set; }
		}

		[Bind(Prefix = "Reply")]
		public class AnswerResponse
		{
			private bool _c;

			public bool Correct
			{
				get
				{
					return _c;
				}
				set
				{
					_c = value;
				}
			}

			public string Message { get; set; } = string.Empty;
		}
	}
}