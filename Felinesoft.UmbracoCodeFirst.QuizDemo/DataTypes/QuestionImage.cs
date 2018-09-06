using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using System.Web;

namespace Marsman.UmbracoCodeFirst.QuizDemo.DocTypes
{
	[DataType]
	public class QuestionImage : ImageCropper, IHtmlString
	{
		[ImageCropProperty(50, 50)]
		public ImageCrop Small { get; set; }

		[ImageCropProperty(100, 100)]
		public ImageCrop Medium { get; set; }

		[ImageCropProperty(200, 200)]
		public ImageCrop Large { get; set; }
	}

	[DataType]
	public class FactoidImage : ImageCropper, IHtmlString
	{
		[ImageCropProperty(100, 100)]
		public ImageCrop Medium { get; set; }

		[ImageCropProperty(200, 200)]
		public ImageCrop Large { get; set; }

		[ImageCropProperty(300, 300)]
		public ImageCrop ExtraLarge { get; set; }
	}
}