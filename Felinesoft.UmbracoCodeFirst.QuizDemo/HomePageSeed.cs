using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.QuizDemo.MediaTypes;
using System.Web;
using System.Diagnostics;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Seeding;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo
{
	[SeedFactory(publishOnCreate: true)]
	public class HomePageSeed : ISeedFactory<DocumentSeed>
	{
		public int SortOrder
		{
			get
			{
				return 0;
			}
		}

		public DocumentSeed GetSeed()
		{
			return Seed.Document
			(
				"Home", new HomePage()
				{
					Content = new HomePage.ContentTab()
					{
						WelcomeParagraph = "Hello!"
					}
				},
					Seed.Document
					(
						"Question Set 1", new QuestionSet()
						{
							Content = new QuestionSet.ContentTab()
							{
								WelcomeParagraph = "QuestionSet 1!"
							}
						},
						GetQuestions()
					),
					Seed.Document
					(
						"Question Set 2", new QuestionSet()
						{
							Content = new QuestionSet.ContentTab()
							{
								WelcomeParagraph = "QuestionSet 2!"
							}
						},
						GetQuestions()
					),
					Seed.Document
					(
						"Question Set 3", new QuestionSet()
						{
							Content = new QuestionSet.ContentTab()
							{
								WelcomeParagraph = "QuestionSet 3!"
							}
						},
						GetQuestions()
					)
				);
		}

		private IEnumerable<DocumentSeed> GetQuestions()
		{
			return new List<DocumentSeed>()
			{
				Seed.Document
				(
					"Question 1", new Question()
					{
						QuestionDetails = new Question.QuestionTab()
						{
							QuestionText = "Question 1a?",
							Answers = "Answer1a1,Answer1a2,Answer1a3,Answer1a4",
							CorrectAnswer = 2
						}
					}
				),
				Seed.Document
				(
					"Question 2", new Question()
					{
						QuestionDetails = new Question.QuestionTab()
						{
							QuestionText = "Question 1b?",
							Answers = "Answer1b1,Answer1b2,Answer1b3,Answer1b4",
							CorrectAnswer = 2
						}
					}
				),
				Seed.Document
				(
					"Question 3", new Question()
					{
						QuestionDetails = new Question.QuestionTab()
						{
							QuestionText = "Question 1c?",
							Answers = "Answer1c1,Answer1c2,Answer1c3,Answer1c4",
							CorrectAnswer = 2
						}
					}
					),
				Seed.Document
				(
					"Question 4", new Question()
					{
						QuestionDetails = new Question.QuestionTab()
						{
							QuestionText = "Question 1d?",
							Answers = "Answer1d1,Answer1d2,Answer1d3,Answer1d4",
							CorrectAnswer = 2
						}
					}
				)
			};
		}
	}
}