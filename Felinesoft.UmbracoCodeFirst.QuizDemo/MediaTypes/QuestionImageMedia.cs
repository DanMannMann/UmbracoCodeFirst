using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.QuizDemo.DocTypes;

namespace Marsman.UmbracoCodeFirst.QuizDemo.MediaTypes
{

	[MediaType(allowAtRoot: true)]
	public class QuestionImageMedia : MediaTypeBase
	{
		[FileUploadProperty]
		public virtual QuestionImage Image { get; set; }

		[FileSizeProperty]
		public virtual Label Size { get; set; }

		[ImageWidthProperty]
		public Label Width { get; set; }

		[ImageHeightProperty]
		public Label Height { get; set; }

		[FileExtensionProperty]
		public Label Type { get; set; }
	}

}