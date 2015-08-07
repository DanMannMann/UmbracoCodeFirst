using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System.Web;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    [MediaType("File", "File", null, "icon-document", false, false, "")]
    [BuiltInMediaType]
    public class MediaFile : MediaFileBase
    {

    }

    public class MediaFileBase : MediaTypeBase, IHtmlString
    {
        public class FileTab : TabBase
        {
            [ContentProperty("Upload file", "umbracoFile", false, "", 0, false)]
            public Upload UploadFile { get; set; }

            [ContentProperty("Type", "umbracoExtension", false, "", 1, false)]
            public Label Type { get; set; }

            [ContentProperty("Size", "umbracoBytes", false, "", 2, false)]
            public Label Size { get; set; }
        }

        [ContentTab("File", 1)]
        public FileTab File { get; set; }

        public string ToHtmlString()
        {
            var toAdd = DataTypeUtils.GetHtmlTagContentFromContextualAttributes(this);
            return File == null || File.UploadFile == null ? string.Empty : "<a" + toAdd + " href='" + File.UploadFile.Url + "'>" + NodeDetails.Name + "</a>";
        }

        public override string ToString()
        {
            return File == null || File.UploadFile == null ? string.Empty : File.UploadFile.Url;
        }
    }
}