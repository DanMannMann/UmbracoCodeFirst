using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.QuizDemo.DocTypes;

namespace Felinesoft.UmbracoCodeFirst.QuizDemo.MediaTypes
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