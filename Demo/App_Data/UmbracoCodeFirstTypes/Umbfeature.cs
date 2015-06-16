
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace Demo.GeneratedUmbracoTypes
{
    [DocumentType(null, "Feature", "umbFeature", null, "developerRuby.gif", false, false, false, "")]
    public class Umbfeature : DocumentTypeBase
    {
        public class ContentTab : TabBase
        {
            [DocumentProperty("Image", "image", "Umbraco.UploadField", "Upload", null, false, "", 0, false)]
            public Demo.GeneratedUmbracoTypes.Upload Image { get; set; }

            [DocumentProperty("Feature Header", "featureHeader", "Umbraco.TextboxMultiple", "Textbox multiple", null, false, "", 1, false)]
            public TextboxMultiple FeatureHeader { get; set; }

            [DocumentProperty("Feature Text", "featureText", "Umbraco.TextboxMultiple", "Textbox multiple", null, false, "", 2, false)]
            public TextboxMultiple FeatureText { get; set; }

        }

        [DocumentTab("Content", 0)]
        public ContentTab Content { get; set; }
    }
}