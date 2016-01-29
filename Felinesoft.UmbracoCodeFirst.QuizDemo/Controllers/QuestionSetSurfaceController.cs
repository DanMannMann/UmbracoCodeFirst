using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Controllers;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	public class QuestionSetSurfaceController : CodeFirstSurfaceController<QuestionSet>, Events.IOnLoad<QuestionSet, QuestionSetViewModel>, Events.IOnCreate<QuestionSet>
	{
		#region Actions
		/// <summary>
		/// Validates the answer
		/// </summary>
		public ActionResult ValidateAnswerAsync(QuestionSetViewModel questionSet)
		{
			//If async only render the relevant partial
			string viewName;
			viewName = CheckAnswer(questionSet);
			return View(viewName, GetViewModel(questionSet, Document));
		}

		/// <summary>
		/// Validates the answer asynchronously
		/// </summary>
		public ActionResult ValidateAnswer(QuestionSetViewModel questionSet, bool isAjax)
		{
			if (isAjax)
			{
				return ValidateAnswerAsync(questionSet);
			}

			//If no async re-render the entire page
			CheckAnswer(questionSet);
			return View("QuestionSet", GetViewModel(questionSet, Document));
		}

		/// <summary>
		/// Gets next question
		/// </summary>
		public ActionResult GetNextQuestionAsync(QuestionSetViewModel questionSet)
		{
			//If async only render the relevant partial
			string viewName;
			viewName = CheckNextQuestion(questionSet);
			return View(viewName, GetViewModel(questionSet, Document));
		}

		/// <summary>
		/// Gets next question asynchronously
		/// </summary>
		public ActionResult GetNextQuestion(QuestionSetViewModel questionSet, bool isAjax)
		{
			if (isAjax)
			{
				return GetNextQuestionAsync(questionSet);
			}

			//If no async re - render the entire page
			CheckNextQuestion(questionSet);
			return View("QuestionSet", GetViewModel(questionSet, Document));
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Constructs a model which can be used as the input to any view in this site. The model is a combination
		/// of Umbraco's render model, the code-first strongly-typed document and a custom viewmodel.
		/// </summary>
		private object GetViewModel(QuestionSetViewModel questionSet, QuestionSet setDoc)
		{
			return new DocumentViewModel<QuestionSet, QuestionSetViewModel>(new RenderModel(setDoc.NodeDetails.PublishedContent), setDoc, questionSet);
		}

		/// <summary>
		/// Checks whether the answer to a question is correct and returns the name
		/// of the view that should be rendered in response to the answer.
		/// </summary>
		private string CheckAnswer(QuestionSetViewModel questionSet)
		{
			string viewName;
			ModelState.Clear();

			var questionDocument = Document.Children.FirstOrDefault(x => x.NodeDetails.UmbracoId == questionSet.QuestionId);
			if (questionSet.Answer.AnswerIndex != questionDocument.CorrectAnswer.Value)
			{
				questionSet.Answer.AnswerIndex = -1;
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Dictionary<QuizmasterDictionary>().IncorrectAnswer };
				viewName = "_questionError";
			}
			else
			{
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Dictionary<QuizmasterDictionary>().CorrectAnswer, Correct = true };
				viewName = "_questionCorrect";
			}

			questionSet.RequiredPartial = viewName;
			return viewName;
		}

		/// <summary>
		/// Works out the next question to display and returns the relevant view name.
		/// </summary>
		private string CheckNextQuestion(QuestionSetViewModel questionSet)
		{
			string viewName;
			ModelState.Clear();
			if (questionSet.Reply?.Correct == true && questionSet.QuestionIndex + 1 > questionSet.MaxIndex)
			{
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Dictionary<QuizmasterDictionary>().NoMoreQuestions };
				viewName = "_questionEnd";
			}
			else
			{
				//Move to next question if answer was correct, otherwise stay on same question.
				questionSet.QuestionIndex = questionSet.Reply?.Correct == true ? questionSet.QuestionIndex + 1 : questionSet.QuestionIndex;
				questionSet.QuestionId = Document.ElementAt(questionSet.QuestionIndex)?.NodeDetails?.UmbracoId ?? -1;
				questionSet.Answer.AnswerIndex = -1;
				viewName = "_question";
			}

			return viewName;
		}
		#endregion

		#region Events
		/// <summary>
		/// An event raised whenever an instance of the QuestionSet document type is created. The method
		/// sets some default values on the document instance.
		/// </summary>
		public bool OnCreate(QuestionSet model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			model.WelcomeParagraph = new RichtextEditor() { Value = "Welcome to the new question set" };
			model.NodeDetails.Name = "New question set";
			return true;
		}

		/// <summary>
		/// An event raised whenever a QuestionSet document is being requested. The method
		/// sets up a default viewmodel, which will be passed back and forth by the form postbacks or
		/// ajax posts on the views.
		/// </summary>
		public void OnLoad(QuestionSet model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out QuestionSetViewModel viewModel)
		{
			viewModel = new QuestionSetViewModel()
			{
				MaxIndex = model?.Count - 1 ?? -1,
				SetId = currentPage?.Id ?? -1,
				QuestionIndex = model?.Count > 0 ? 0 : -1,
				QuestionId = model.FirstOrDefault()?.NodeDetails?.UmbracoId ?? -1,
				Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1
				}
			};
		}
		#endregion
	}
}