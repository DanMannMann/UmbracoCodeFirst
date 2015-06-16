
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
    [DocumentType(null, "Home", "umbHomePage", new Type[] { typeof(Umbfeature), typeof(Umbtextpage) }, ".sprTreeSettingDomain", true, false, false, "")]
    public class Umbhomepage : ListViewDocumentType
    {
        public class SocialTab : TabBase
        {
            [DocumentProperty("Facebook link", "facebookLink", "Umbraco.Textbox", "Textstring", null, false, "", 0, false)]
            public string FacebookLink { get; set; }

            [DocumentProperty("Twitter link", "twitterLink", "Umbraco.Textbox", "Textstring", null, false, "", 1, false)]
            public string TwitterLink { get; set; }

            [DocumentProperty("Pinterest", "pinterestLink", "Umbraco.Textbox", "Textstring", null, false, "", 2, false)]
            public string Pinterest { get; set; }

            [DocumentProperty("Dribbble link", "dribbbleLink", "Umbraco.Textbox", "Textstring", null, false, "", 3, false)]
            public string DribbbleLink { get; set; }

            [DocumentProperty("LinkedIn link", "linkedInLink", "Umbraco.Textbox", "Textstring", null, false, "", 4, false)]
            public string LinkedinLink { get; set; }

            [DocumentProperty("Google+ link", "googleLink", "Umbraco.Textbox", "Textstring", null, false, "", 5, false)]
            public string GoogleLink { get; set; }

        }
        public class BannerTab : TabBase
        {
            [DocumentProperty("Hide banner?", "hideBanner", "Umbraco.TrueFalse", "True/false", null, false, "", 0, false)]
            public bool HideBanner { get; set; }

            [DocumentProperty(SortOrder = 1)]
            public Textstring BannerHeader { get; set; }

            [DocumentProperty(SortOrder = 2)]
            public RichtextEditor BannerText { get; set; }

            [DocumentProperty(SortOrder = 3)]
            public Textstring BannerLinkText { get; set; }
        }
        public class ContentTab : TabBase
        {
            [DocumentProperty("Site Name", "siteName", "Umbraco.Textbox", "Textstring", null, false, "", 0, false)]
            public string SiteName { get; set; }

            [DocumentProperty("Byline", "byline", "Umbraco.Textbox", "Textstring", null, false, "", 1, false)]
            public string Byline { get; set; }

            [DocumentProperty("Copyright", "copyright", "Umbraco.Textbox", "Textstring", null, false, "", 2, false)]
            public string Copyright { get; set; }

            [DocumentProperty("Continue Button Text", "continueButtonText", "Umbraco.Textbox", "Textstring", null, false, "", 3, false)]
            public string ContinueButtonText { get; set; }

        }

        [DocumentTab("Social", 0)]
        public SocialTab Social { get; set; }

        [DocumentTab("Banner", 1)]
        public BannerTab Banner { get; set; }

        [DocumentTab("Content", 2)]
        public ContentTab Content { get; set; }
    }
}