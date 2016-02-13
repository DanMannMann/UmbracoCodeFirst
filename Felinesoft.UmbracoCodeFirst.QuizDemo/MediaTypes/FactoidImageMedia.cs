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
	public class FactoidImageMedia : MediaTypeBase
	{
		[FileUploadProperty]
		public virtual FactoidImage Image { get; set; }

		[FileSizeProperty]
		public virtual Label Size { get; set; }
	}
}