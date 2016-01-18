using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
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
}