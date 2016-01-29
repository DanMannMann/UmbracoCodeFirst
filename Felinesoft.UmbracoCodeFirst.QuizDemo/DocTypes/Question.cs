using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Linq;
using System.Web.Http.Routing;
using System.Web;
using System.Net.Http;
using System;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	[DocumentType(allowAtRoot: false)]
	public class Question : DocumentTypeBase
	{
		[ContentProperty(mandatory: true)]
		public virtual Textstring QuestionText { get; set; }

		[ContentProperty(mandatory: true)]
		[InstancePreValue("minItems", "4")]
		[InstancePreValue("maxItems", "4")]
		public virtual MultipleTextstring Answers { get; set; }

		[ContentProperty(mandatory: false)]
		public virtual QuestionImage Image { get; set; }

		[ContentProperty(mandatory: true)]
		[InstancePreValue("max", "4")]
		[InstancePreValue("min", "1")]
		public virtual Numeric CorrectAnswer { get; set; }

		public QuestionSet Parent
		{
			get
			{
				return this.NearestAncestor<QuestionSet>();
			}
		}

		public string ImageUrl
		{
			get
			{
				string imgUrl = null;
				try
				{
					imgUrl = Image.Medium.Effects.RoundedCorners(30, System.Drawing.Color.White).ToString();
				}
				catch
				{
					try
					{
						imgUrl = Parent.DefaultImageForSet.Medium.Effects.RoundedCorners(30, System.Drawing.Color.White).ToString();
					}
					catch
					{
						imgUrl = null;
					}
				}
				return imgUrl;
			}
		}
	}
}