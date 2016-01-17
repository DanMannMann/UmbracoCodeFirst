using System.Collections.Generic;
using System.Linq;
using RJP.MultiUrlPicker.Models;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using at = Felinesoft.UmbracoCodeFirst.Attributes;
using dt = Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using Felinesoft.UmbracoCodeFirst.Events;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	[DocumentType]
	[Template(true)]
	[EventHandler(typeof(DebugController))]
	public class Debug : DocumentTypeBase
	{
		#region Umbraco Properties
		[ContentProperty]
		public TrueFalse Boolean { get; set; }

		[ContentProperty]
		public DatePickerWithTime Timer { get; set; }

		[ContentProperty]
		public RjpUrl Method1 { get; set; }

		[ContentProperty]
		public RelatedLinks OtherOne { get; set; }

		[DocumentPickerProperty]
		[NodePickerConfig(minimumItems: 4, showEditButton: true, showOpenButton: true, showPathsWhenHovering: true)]
		public virtual IEnumerable<Debug> PickedOnes3 { get; set; }
		#endregion
	}

	[DocumentType]
	[Template(true, alias: "debug")]
	[EventHandler(typeof(DebugController))]
	public class ExtraDebug : Debug
	{
		[ContentProperty]
		public Textstring Textses { get; set; }
	}

	public class DebugController : EventHandler<Debug, DebugViewModel>
	{
		public override bool OnCreate(Debug instance, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			instance.NodeDetails.Name = "SUCKA";
			instance.Timer = new DatePickerWithTime()
			{
				Value = DateTime.Now.AddDays(-2)
			};
			instance.OtherOne = new RelatedLinks()
			{
				Items = new List<RelatedLink>()
				{
					new RelatedLink()
					{
						Caption = "Yo momma",
						IsInternal = false,
						Title = "I eat pants",
						Url = "http://our.umbraco.org",
						NewWindow = true
					}
				}
			};
			return true;
		}

		public override void OnRender(Debug model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out DebugViewModel viewModel)
		{
			viewModel = new DebugViewModel()
			{
				BooleanValueBackwards = model?.Boolean?.ToString()?.ReverseGraphemeClusters()
			};
		}

		public override bool OnSave(Debug instance, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			instance.NodeDetails.Name = "HAHA FUCKER";
			return true;
		}

		public override bool OnDelete(Debug instance, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return false;
		}
	}
}
