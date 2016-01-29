using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Debug.DocTypes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace Felinesoft.UmbracoCodeFirst.Debug
{
	public class StartUp : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			CodeFirstManager.Current.Initialise(GetType().Assembly);

			//Check if default homepage exists
			if (applicationContext.Services.ContentService.GetRootContent().Count() == 0)
			{
				//Default homepage doesn't exist, add the seed content
				AddSeedContent();
				applicationContext.Services.ContentService.RePublishAll();
				umbraco.library.RefreshContent();
			}
		}

		#region Seed Methods
		private void AddSeedContent()
		{
			var homePage = new HomePage();
			homePage.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "Welcome to the Felinesoft Code-First quiz. This quiz tests your knowledge of Felinesoft Code-First for Umbraco." };
			homePage.NodeDetails.Name = "Code-First Quiz";
			homePage.Persist(publish: true);
			CreateSet1(homePage);
			CreateSet2(homePage);
			CreateSet3(homePage);
		}

		private void CreateSet1(HomePage homePage)
		{
			var questionSet1 = new QuestionSet();
			questionSet1.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "This question set tests your knowledge of declaring and using code-first document types." };
			questionSet1.NodeDetails.Name = "Document Type Quiz";
			questionSet1.Persist(parentId: homePage.NodeDetails.UmbracoId, publish: true);

			var set1_q1 = CreateQuestion
			(
				"Document type attribute",
				"What is the correct attribute to use to signal that a class represents a document type?",
				"[DocumentType]",
				"[ContentType]",
				"[DocumentClass]",
				"[CodeFirstDocument]",
				1
			);
			set1_q1.Persist(parentId: questionSet1.NodeDetails.UmbracoId, publish: true);

			var set1_q2 = CreateQuestion
			(
				"Document type base",
				"What is the correct base class to inherit from when declaring a document type class?",
				"CodeFirstDocumentType",
				"ContentTypeBase",
				"BaseDocumentType",
				"DocumentTypeBase",
				4
			);
			set1_q2.Persist(parentId: questionSet1.NodeDetails.UmbracoId, publish: true);

			var set1_q3 = CreateQuestion
			(
				"Property type",
				"What is the correct attribute to use to signal that a property of a class represents an Umbraco property type?",
				"[Property]",
				"[ContentProperty]",
				"[DocumentProperty]",
				"[CodeFirstProperty]",
				2
			);
			set1_q3.Persist(parentId: questionSet1.NodeDetails.UmbracoId, publish: true);
		}

		private void CreateSet2(HomePage homePage)
		{
			var questionSet2 = new QuestionSet();
			questionSet2.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "This question set tests your knowledge of declaring and using code-first data types." };
			questionSet2.NodeDetails.Name = "Data Type Quiz";
			questionSet2.Persist(parentId: homePage.NodeDetails.UmbracoId, publish: true);

			var set2_q1 = CreateQuestion
			(
				"Data type attribute",
				"What is the correct attribute to use to signal that a class represents a data type?",
				"[Type]",
				"[CodeFirstData]",
				"[UmbracoDataType]",
				"[DataType]",
				4
			);
			set2_q1.Persist(parentId: questionSet2.NodeDetails.UmbracoId, publish: true);

			var set2_q2 = CreateQuestion
			(
				"Data type interfaces",
				"Which of the following is the correct interface to implement to create a data type which is stored as a string and can accomodate a large amount of data?",
				"IUmbracoNtextDataType",
				"IUmbracoStringDataType",
				"IUmbracoDataTypeNtext",
				"IUmbracoDataTypeString",
				1
			);
			set2_q2.Persist(parentId: questionSet2.NodeDetails.UmbracoId, publish: true);

			var set2_q3 = CreateQuestion
			(
				"Data type implementation",
				"When is it OK to leave the interface methods unimplemented on a data type?",
				"When strongly-typed views are not being used, or that data type is not being used within them",
				"When the data value is always null/default",
				"For any data value which is stored as a string in the database",
				"Never",
				1
			);
			set2_q3.Persist(parentId: questionSet2.NodeDetails.UmbracoId, publish: true);
		}

		private void CreateSet3(HomePage homePage)
		{
			var questionSet3 = new QuestionSet();
			questionSet3.WelcomeParagraph = new DataTypes.BuiltIn.RichtextEditor() { Value = "This question set tests your knowledge of declaring and using code-first dictionaries." };
			questionSet3.NodeDetails.Name = "Dictionary Quiz";
			questionSet3.Persist(parentId: homePage.NodeDetails.UmbracoId, publish: true);

			var set3_q1 = CreateQuestion
			(
				"Dictionary attribute",
				"What is the correct attribute to use to signal that a class represents a dictionary?",
				"[Dictionary]",
				"[CodeFirstDictionary]",
				"[Dict]",
				"[UmbracoDictionary]",
				1
			);
			set3_q1.Persist(parentId: questionSet3.NodeDetails.UmbracoId, publish: true);

			var set3_q2 = CreateQuestion
			(
				"Dictionary items",
				"What is the correct attribute to use to signal that a property represents a dictionary item?",
				"[DictItem]",
				"[DictionaryItem]",
				"[Item]",
				"[DictionaryEntry]",
				3
			);
			set3_q2.Persist(parentId: questionSet3.NodeDetails.UmbracoId, publish: true);

			var set3_q3 = CreateQuestion
			(
				"Dictionary base class",
				"What is the correct base class to inherit from when declaring a dictionary class?",
				"CodeFirstDictionaryBase",
				"DictBase",
				"UmbracoDictionary",
				"DictionaryBase",
				4
			);
			set3_q3.Persist(parentId: questionSet3.NodeDetails.UmbracoId, publish: true);
		}

		private static Question CreateQuestion(string questionName, string questionText, string answer1, string answer2, string answer3, string answer4, int correctAnswer)
		{
			var question = new Question();
			question.QuestionText = new DataTypes.BuiltIn.Textstring() { Value = questionText };
			question.Answers = new DataTypes.BuiltIn.MultipleTextstring()
			{
				answer1,
				answer2,
				answer3,
				answer4
			};
			question.CorrectAnswer = new DataTypes.BuiltIn.Numeric() { Value = correctAnswer };
			question.NodeDetails.Name = questionName;
			return question;
		}
		#endregion
	}
}
