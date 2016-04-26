using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Linq;
using Felinesoft.UmbracoCodeFirst.QuizDemo.MediaTypes;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes
{
	[DocumentType(allowAtRoot: false)]
	public class Question : DocumentTypeBase
	{
		#region Tab Definitions
		public class QuestionTab : TabBase
		{
			[ContentProperty(mandatory: true)]
			public virtual Textstring QuestionText { get; set; }

			[ContentProperty(mandatory: true)]
			[InstancePreValue("minItems", "4")]
			[InstancePreValue("maxItems", "4")]
			public virtual MultipleTextstring Answers { get; set; }

			[ContentProperty(mandatory: true)]
			[InstancePreValue("max", "4")]
			[InstancePreValue("min", "1")]
			public virtual Numeric CorrectAnswer { get; set; }
		}

		public class ExtrasTab : TabBase
		{
			[ContentProperty(mandatory: false)]
			public virtual SingleMediaPicker<QuestionImageMedia> Image { get; set; }

			[ContentProperty]
			[NodePickerConfig(maximumItems: 3, startNode: "/Factoids", showEditButton: true, showOpenButton: true, showPathsWhenHovering: true)]
			[AltText("Picker Made This")]
			public DocumentPicker<Factoid> Factoids { get; set; } = new DocumentPicker<Factoid>();
		}
		#endregion

		#region Tab Declarations
		[ContentTab]
		public QuestionTab QuestionDetails { get; set; }

		[ContentTab]
		public ExtrasTab Extras { get; set; }
		#endregion

		#region Non-Umbraco Properties
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
					imgUrl = Extras.Image.PickedItem.Image.Medium.Effects.RoundedCorners(30, System.Drawing.Color.White).ToString();
				}
				catch
				{
					try
					{
						imgUrl = Parent.Content.DefaultImageForSet.PickedItem.Image.Medium.Effects.RoundedCorners(30, System.Drawing.Color.White).ToString();
					}
					catch
					{
						imgUrl = null;
					}
				}
				return imgUrl;
			}
		}
		#endregion
	}

}