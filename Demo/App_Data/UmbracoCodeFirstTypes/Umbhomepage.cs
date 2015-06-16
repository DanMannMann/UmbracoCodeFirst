
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
    public class Umbhomepage : DocumentTypeBase
    {
        public class SocialTab : TabBase
        {
            [DocumentProperty("Facebook link", "facebookLink", "Umbraco.Textbox", "Textstring", null, false, "", 0, false)]
            public Textstring FacebookLink { get; set; }

            [DocumentProperty("Twitter link", "twitterLink", "Umbraco.Textbox", "Textstring", null, false, "", 1, false)]
            public Textstring TwitterLink { get; set; }

            [DocumentProperty("Pinterest", "pinterestLink", "Umbraco.Textbox", "Textstring", null, false, "", 2, false)]
            public Textstring Pinterest { get; set; }

            [DocumentProperty("Dribbble link", "dribbbleLink", "Umbraco.Textbox", "Textstring", null, false, "", 3, false)]
            public Textstring DribbbleLink { get; set; }

            [DocumentProperty("LinkedIn link", "linkedInLink", "Umbraco.Textbox", "Textstring", null, false, "", 4, false)]
            public Textstring LinkedinLink { get; set; }

            [DocumentProperty("Google+ link", "googleLink", "Umbraco.Textbox", "Textstring", null, false, "", 5, false)]
            public Textstring Google+Link { get; set; }

        }
        public class BannerTab : TabBase
        {
            [DocumentProperty("Hide banner?", "hideBanner", "Umbraco.TrueFalse", "True/false", null, false, "", 0, false)]
            public TrueFalse HideBanner? { get; set; }

            [DocumentProperty("Banner Header", "bannerheader", "Umbraco.Textbox", "Textstring", null, false, "", 4, false)]
            public Textstring BannerHeader { get; set; }

            [DocumentProperty("Banner Text", "bannertext", "Umbraco.TinyMCEv3", "Richtext editor", null, false, "", 5, false)]
            public RichtextEditor BannerText { get; set; }

            [DocumentProperty("Banner Link Text", "bannerlinktext", "Umbraco.Textbox", "Textstring", null, false, "", 6, false)]
            public Textstring BannerLinkText { get; set; }

        }
        public class ContentTab : TabBase
        {
            [DocumentProperty("Site Name", "siteName", "Umbraco.Textbox", "Textstring", null, false, "", 0, false)]
            public Textstring SiteName { get; set; }

            [DocumentProperty("Byline", "byline", "Umbraco.Textbox", "Textstring", null, false, "", 1, false)]
            public Textstring Byline { get; set; }

            [DocumentProperty("Copyright", "copyright", "Umbraco.Textbox", "Textstring", null, false, "", 2, false)]
            public Textstring Copyright { get; set; }

            [DocumentProperty("Continue Button Text", "continueButtonText", "Umbraco.Textbox", "Textstring", null, false, "", 3, false)]
            public Textstring ContinueButtonText { get; set; }

        }

        [DocumentTab("Social", 0)]
        public SocialTab Social { get; set; }

        [DocumentTab("Banner", 1)]
        public BannerTab Banner { get; set; }

        [DocumentTab("Content", 2)]
        public ContentTab Content { get; set; }
    }
}