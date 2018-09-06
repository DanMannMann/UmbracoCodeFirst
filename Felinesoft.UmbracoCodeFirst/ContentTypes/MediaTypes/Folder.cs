using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.ContentTypes;

namespace Marsman.UmbracoCodeFirst.ContentTypes
{
    [CodeFirstCommonBase]
    public abstract class MediaFolderBase : MediaTypeBase
    {
		public class ContentsTab : TabBase
		{
			[ContentProperty(@"Contents:", @"contents", false, @"", 0, false)]
			public virtual Marsman.UmbracoCodeFirst.DataTypes.BuiltIn.ListView_Media Contents { get; set; }

		}

		[ContentTab(@"Contents", 1)]
		public virtual ContentsTab Contents { get; set; }
	}

    [MediaType("Folder", "Folder", new Type[] { typeof(MediaFolder), typeof(MediaImage), typeof(MediaFile) }, "icon-folder", true, false, "")]
    [BuiltInMediaType]
    public class MediaFolder : MediaFolderBase
    {

    }
}