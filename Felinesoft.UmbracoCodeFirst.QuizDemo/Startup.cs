using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.QuizDemo.MediaTypes;
using System.Web;
using System.Diagnostics;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo
{
	public class StartUp : ApplicationEventHandler
	{
		private Factoids _factoidFolder;
		private List<Factoid> _factoids = new List<Factoid>();
		private Random _randy = new Random();
		private FactoidImageMedia _image;

		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			CodeFirstManager.Current.Features.HideCodeFirstEntityTypesInTrees = false;
			CodeFirstManager.Current.Features.UseConcurrentInitialisation = true; //note: this should be explicitly set to false in load-balanced/farm deployments due to a concurrency bug in Umbraco core (seen in 7.2.1)
			CodeFirstManager.Current.Initialise(GetType().Assembly);
			var s = sw.ElapsedMilliseconds;
			//Check if default homepage exists
			if (applicationContext.Services.ContentService.GetRootContent().Count() == 0)
			{
				//Default homepage doesn't exist, add the seed content
				AddSeedContent();
				applicationContext.Services.ContentService.RePublishAll();
				umbraco.library.RefreshContent();
			}
			sw.Stop();
		}

		#region Seed Methods
		private void AddSeedContent()
		{
			//try
			{
				var homePage = new HomePage();
				homePage.Content = new HomePage.ContentTab();
				homePage.Content.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "Welcome to the Felinesoft Code-First quiz. This quiz tests your knowledge of Felinesoft Code-First for Umbraco." };
				homePage.NodeDetails.Name = "Code-First Quiz";
				homePage.Persist(publish: true);

				CreateFactoids();

				CreateSet1(homePage);
				CreateSet2(homePage);
				CreateSet3(homePage);
			}
			//catch
			//{
			//	foreach(var c in ApplicationContext.Current.Services.ContentService.GetRootContent())
			//	{
			//		ApplicationContext.Current.Services.ContentService.Delete(c);
			//	}
			//	throw;
			//}
		}

		private void CreateFactoids()
		{
			_image = new FactoidImageMedia();
			_image.NodeDetails.Name = "FactoidImage";
			using (var fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/MediaTypes/logoimg.png"), System.IO.FileMode.Open))
			{
				_image.SetMediaFile(fs);
				_image.Persist();
			}
			_factoidFolder = new Factoids();
			_factoidFolder.NodeDetails.Name = "Factoids";
			_factoidFolder.Persist(publish: true);

			CreateFactoid(
				"Man these facts are old",
				"By the year 2012 there will be approximately 17 billion devices connected to the Internet.");

			CreateFactoid(
				"(Of which 870,000 are clickbait listicle slideshow sites)",
				"Domain names are being registered at a rate of more than one million names every month.");

			CreateFactoid(
				"I think this fact is a bit tame, compared to facebook being more populous than China.",
				"MySpace reports over 110 million registered users. Were it a country, it would be the tenth largest, just behind Mexico.");

			CreateFactoid(
				"Love & Marriage",
				"One of every 8 married couples in the US last year met online.");

			CreateFactoid(
				"Blinkin' 'eck",
				"The average computer user blinks 7 times a minute, less than half the normal rate of 20.");

			CreateFactoid(
				"The Mother of All Demos",
				"The first computer mouse was invented by Doug Engelbart in around 1964 and was made of wood. His initial demonstration of the mouse also included the first ever demonstrations of word processing, computer networking and video-conferencing. In the late '60s.");

			CreateFactoid(
				"The Internets Go Quick",
				"While it took the radio 38 years, and the television a short 13 years, it took the World Wide Web only 4 years to reach 50 million users.");

			CreateFactoid(
				"Grace Hopper is Definitely the Coolest Person Ever",
				"Grace Hopper, the inventor of the first ever software compiler and co-creator of COBOL, was also the oldest serviceperson on active duty in the US Navy, retiring at the age of 79 and rank of Rear Admiral.");
		}

		private void CreateFactoid(string title, string text)
		{
			
			var factoid = new Factoid();
			factoid.Details = new Factoid.FactoidDetailsTab();
			factoid.Details.Image = _image;
			factoid.Details.FactoidTitle = new DataTypes.BuiltIn.Textstring() { Value = title };
			factoid.Details.FactoidText = new DataTypes.BuiltIn.Textstring() { Value = text };
			factoid.NodeDetails.Name = factoid.Details.FactoidTitle.Value.Truncate(10);
			factoid.Persist(_factoidFolder?.NodeDetails?.UmbracoId ?? -1, publish: true);
			_factoids.Add(factoid);

		}

		private void CreateSet1(HomePage homePage)
		{
			var questionSet1 = new QuestionSet();
			questionSet1.Content = new QuestionSet.ContentTab();
			questionSet1.Content.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "This question set tests your knowledge of declaring and using code-first document types." };
			questionSet1.NodeDetails.Name = "Document Type Quiz";
			questionSet1.Persist(parentId: homePage.NodeDetails.UmbracoId, publish: true);

			var question1 = CreateQuestion
			(
				"Document type attribute",
				"What is the correct attribute to use to signal that a class represents a document type?",
				"[DocumentType]",
				"[ContentType]",
				"[DocumentClass]",
				"[CodeFirstDocument]",
				1
			);
			question1.Extras = new Question.ExtrasTab();
			question1.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question1.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question1.Persist(parentId: questionSet1.NodeDetails.UmbracoId, publish: true);

			var question2 = CreateQuestion
			(
				"Document type base",
				"What is the correct base class to inherit from when declaring a document type class?",
				"CodeFirstDocumentType",
				"ContentTypeBase",
				"BaseDocumentType",
				"DocumentTypeBase",
				4
			);
			question2.Extras = new Question.ExtrasTab();
			question2.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question2.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question2.Persist(parentId: questionSet1.NodeDetails.UmbracoId, publish: true);

			var question3 = CreateQuestion
			(
				"Property type",
				"What is the correct attribute to use to signal that a property of a class represents an Umbraco property type?",
				"[Property]",
				"[ContentProperty]",
				"[DocumentProperty]",
				"[CodeFirstProperty]",
				2
			);
			question3.Extras = new Question.ExtrasTab();
			question3.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question3.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question3.Persist(parentId: questionSet1.NodeDetails.UmbracoId, publish: true);
		}

		private void CreateSet2(HomePage homePage)
		{
			var questionSet2 = new QuestionSet();
			questionSet2.Content = new QuestionSet.ContentTab();
			questionSet2.Content.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "This question set tests your knowledge of declaring and using code-first data types." };
			questionSet2.NodeDetails.Name = "Data Type Quiz";
			questionSet2.Persist(parentId: homePage.NodeDetails.UmbracoId, publish: true);

			var question1 = CreateQuestion
			(
				"Data type attribute",
				"What is the correct attribute to use to signal that a class represents a data type?",
				"[Type]",
				"[CodeFirstData]",
				"[UmbracoDataType]",
				"[DataType]",
				4
			);
			question1.Extras = new Question.ExtrasTab();
			question1.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question1.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question1.Persist(parentId: questionSet2.NodeDetails.UmbracoId, publish: true);

			var question2 = CreateQuestion
			(
				"Data type interfaces",
				"Which of the following is the correct interface to implement to create a data type which is stored as a string and can accomodate a large amount of data?",
				"IUmbracoNtextDataType",
				"IUmbracoStringDataType",
				"IUmbracoDataTypeNtext",
				"IUmbracoDataTypeString",
				1
			);
			question2.Extras = new Question.ExtrasTab();
			question2.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question2.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question2.Persist(parentId: questionSet2.NodeDetails.UmbracoId, publish: true);

			var question3 = CreateQuestion
			(
				"Data type implementation",
				"When is it OK to leave the interface methods unimplemented on a data type?",
				"When strongly-typed views are not being used, or that data type is not being used within them",
				"When the data value is always null/default",
				"For any data value which is stored as a string in the database",
				"Never",
				1
			);
			question3.Extras = new Question.ExtrasTab();
			question3.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question3.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question3.Persist(parentId: questionSet2.NodeDetails.UmbracoId, publish: true);
		}

		private void CreateSet3(HomePage homePage)
		{
			var questionSet3 = new QuestionSet();
			questionSet3.Content = new QuestionSet.ContentTab();
			questionSet3.Content.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "This question set tests your knowledge of declaring and using code-first dictionaries." };
			questionSet3.NodeDetails.Name = "Dictionary Quiz";
			questionSet3.Persist(parentId: homePage.NodeDetails.UmbracoId, publish: true);

			var question1 = CreateQuestion
			(
				"Dictionary attribute",
				"What is the correct attribute to use to signal that a class represents a dictionary?",
				"[Dictionary]",
				"[CodeFirstDictionary]",
				"[Dict]",
				"[UmbracoDictionary]",
				1
			);
			question1.Extras = new Question.ExtrasTab();
			question1.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question1.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question1.Persist(parentId: questionSet3.NodeDetails.UmbracoId, publish: true);

			var question2 = CreateQuestion
			(
				"Dictionary items",
				"What is the correct attribute to use to signal that a property represents a dictionary item?",
				"[DictItem]",
				"[DictionaryItem]",
				"[Item]",
				"[DictionaryEntry]",
				3
			);
			question2.Extras = new Question.ExtrasTab();
			question2.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question2.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question2.Persist(parentId: questionSet3.NodeDetails.UmbracoId, publish: true);

			var question3 = CreateQuestion
			(
				"Dictionary base class",
				"What is the correct base class to inherit from when declaring a dictionary class?",
				"CodeFirstDictionaryBase",
				"DictBase",
				"UmbracoDictionary",
				"DictionaryBase",
				4
			);
			question3.Extras = new Question.ExtrasTab();
			question3.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question3.Extras.Factoids.Add(_factoids.ElementAt(_randy.Next(0, _factoids.Count)));
			question3.Persist(parentId: questionSet3.NodeDetails.UmbracoId, publish: true);
		}

		private static Question CreateQuestion(string questionName, string questionText, string answer1, string answer2, string answer3, string answer4, int correctAnswer)
		{
			var question = new Question();
			question.QuestionDetails = new Question.QuestionTab();
			question.QuestionDetails.QuestionText = new DataTypes.BuiltIn.Textstring() { Value = questionText };
			question.QuestionDetails.Answers = new DataTypes.BuiltIn.MultipleTextstring()
			{
				answer1,
				answer2,
				answer3,
				answer4
			};
			question.QuestionDetails.CorrectAnswer = new DataTypes.BuiltIn.Numeric() { Value = correctAnswer };
			question.NodeDetails.Name = questionName;
			return question;
		}
		#endregion
	}
}
