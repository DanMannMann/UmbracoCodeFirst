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

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	public class QuestionSetSurfaceController : CodeFirstSurfaceController<QuestionSet>, Events.IOnLoad<QuestionSet, QuestionSetViewModel>, Events.IOnCreate<QuestionSet>
	{
		public ActionResult ValidateAnswer(QuestionSetViewModel questionSet, bool isAjax = false)
		{
			var questionDocument = Document.Children.FirstOrDefault(x => x.NodeDetails.UmbracoId == questionSet.QuestionId);
			if (questionSet.SetVersion != CurrentPage.Version)
			{
				questionSet.Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1
				};
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Document.Messages.SetVersionChangedErrorMessage.Value };
				return View(ConfigureViewName(questionSet, isAjax, "_questionError"), GetViewModel(questionSet, Document));
			}
			else if (questionSet.QuestionVersion != questionDocument.NodeDetails.PublishedContent.Version)
			{
				questionSet.Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1
				};
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Document.Messages.QuestionVersionChangedErrorMessage.Value };
				return View(ConfigureViewName(questionSet, isAjax, "_questionError"), GetViewModel(questionSet, Document));
			}
			else if (questionSet.Answer.AnswerIndex != questionDocument.CorrectAnswer.Value)
			{
				questionSet.Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1
				};
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Document.Messages.IncorrectAnswerMessage.Value };
				return View(ConfigureViewName(questionSet, isAjax, "_questionError"), GetViewModel(questionSet, Document));
			}
			else
			{
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Document.Messages.CorrectAnswerMessage.Value, Correct = true };
				return View(ConfigureViewName(questionSet, isAjax, "_questionCorrect"), GetViewModel(questionSet, Document));
			}
		}

		public ActionResult GetNextQuestion(QuestionSetViewModel questionSet, bool isAjax = false)
		{
			if (questionSet.SetVersion != CurrentPage.Version)
			{
				questionSet.Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1
				};
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Document.Messages.SetVersionChangedErrorMessage.Value };
				return View(ConfigureViewName(questionSet, isAjax, "_questionError"), GetViewModel(questionSet, Document));
			}
			else if (questionSet.Reply?.Correct == true && questionSet.QuestionIndex + 1 > questionSet.MaxIndex)
			{
				questionSet.Reply = new QuestionSetViewModel.AnswerResponse() { Message = Document.Messages.NoMoreQuestionsMessage.Value };
				return View(ConfigureViewName(questionSet, isAjax, "_questionEnd"), GetViewModel(questionSet, Document));
			}
			else
			{
				questionSet.QuestionIndex = questionSet.Reply?.Correct == true ? questionSet.QuestionIndex + 1 : questionSet.QuestionIndex;
				questionSet.QuestionId = Document.ElementAt(questionSet.QuestionIndex)?.NodeDetails?.UmbracoId ?? -1;
				questionSet.QuestionVersion = Document.ElementAt(questionSet.QuestionIndex)?.NodeDetails?.PublishedContent?.Version ?? Guid.Empty;
				questionSet.Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1,
				};
				return View(ConfigureViewName(questionSet, isAjax, "_question"), GetViewModel(questionSet, Document));
			}
		}

		public void OnLoad(QuestionSet model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out QuestionSetViewModel viewModel)
		{
			viewModel = new QuestionSetViewModel()
			{
				MaxIndex = model?.Count - 1 ?? -1,
				SetVersion = currentPage?.Version ?? Guid.Empty,
				SetId = currentPage?.Id ?? -1,
				QuestionIndex = model?.Count > 0 ? 0 : -1,
				QuestionId = model.FirstOrDefault()?.NodeDetails?.UmbracoId ?? -1,
				QuestionVersion = model.FirstOrDefault()?.NodeDetails?.PublishedContent?.Version ?? Guid.Empty,
				Answer = new QuestionSetViewModel.QuestionResponse()
				{
					AnswerIndex = -1
				}
			};
		}

		private string ConfigureViewName(QuestionSetViewModel questionSet, bool isAjax, string name)
		{
			questionSet.RequiredPartial = name;
			return isAjax ? name : "QuestionSet"; //If not AJAX render the outermost view
		}

		private object GetViewModel(QuestionSetViewModel questionSet, QuestionSet setDoc)
		{
			return new DocumentViewModel<QuestionSet, QuestionSetViewModel>(new Umbraco.Web.Models.RenderModel(setDoc.NodeDetails.PublishedContent), setDoc, questionSet);
		}

		public bool OnCreate(QuestionSet model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			model.Messages.IncorrectAnswerMessage = new Textstring() { Value = "You suck balls, guy." };
			model.Messages.NoMoreQuestionsMessage = new Textstring() { Value = "No more questions! :(" };
			model.Messages.QuestionVersionChangedErrorMessage = new Textstring() { Value = "The question changed. :(" };
			model.Messages.SetVersionChangedErrorMessage = new Textstring() { Value = "The question set changed. :(" };
			model.Messages.CorrectAnswerMessage = new Textstring() { Value = "Woot woot!" };
			return true;
		}
	}
}