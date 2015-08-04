
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    [DocumentType(icon: BuiltInIcons.SprTreeFolder, allowAtRoot: true, allowedChildren: new Type[] { typeof(Genericpage), typeof(TestPage) })]
    [Template(isDefault: true)]
    public class Home : Master
    {
        public override IEnumerable<NavigationItem> NavigationItems
        {
            get
            {
                var lst = NodeDetails.PublishedContent.Children.Where("Visible").Select(x => new NavigationItem() { Url = x.Url, Name = x.Name }).ToList();
                lst.Insert(0, new NavigationItem() { Url = "/", Name = "Home" });
                return lst;
            }
        }

        public class BannerTab : TabBase
        {
            [ContentProperty("Site Name", "siteName", true, "", 0, false)]
            public Textstring SiteName { get; set; }

            [ContentProperty("Line 1", "line1", false, "", 1, false)]
            public Textstring Line1 { get; set; }

            [ContentProperty("Line 2", "line2", false, "", 2, false)]
            public Textstring Line2 { get; set; }

            [ContentProperty("Line 3", "line3", false, "", 3, false)]
            public Textstring Line3 { get; set; }

            [ContentProperty("Button Text", "buttonText", false, "", 4, false)]
            public Textstring ButtonText { get; set; }

            [ContentProperty("Button URL", "buttonUrl", false, "", 5, false)]
            public LegacyContentPicker ButtonURL { get; set; }

            [ContentProperty("Learn More", "learnMore", false, "", 6, false)]
            public Textstring LearnMore { get; set; }

        }
        public class CallToActionTab : TabBase
        {
            [ContentProperty("Title6", "title6", false, "", 0, false)]
            public Textstring Title6 { get; set; }

            [ContentProperty("Description6", "description6", false, "", 1, false)]
            public TextboxMultiple Description6 { get; set; }

            [ContentProperty("Button Text 1", "buttonText1", false, "", 2, false)]
            public Textstring ButtonText1 { get; set; }

            [ContentProperty("Button Text 2", "buttonText2", false, "", 3, false)]
            public Textstring ButtonText2 { get; set; }

            [ContentProperty("Button URL 1", "buttonUrl1", false, "", 4, false)]
            public LegacyContentPicker ButtonURL1 { get; set; }

            [ContentProperty("Button URL 2", "buttonUrl2", false, "", 5, false)]
            public LegacyContentPicker ButtonURL2 { get; set; }

        }
        public class FooterTab : TabBase
        {
            [ContentProperty("Copyright", "copyright", false, "", 0, false)]
            public Textstring Copyright { get; set; }

        }
        public class SocialTab : TabBase
        {
            [ContentProperty("Facebook Url", "facebookUrl", false, "", 0, false)]
            public Textstring FacebookUrl { get; set; }

            [ContentProperty("Twitter Url", "twitterUrl", false, "", 1, false)]
            public Textstring TwitterUrl { get; set; }

            [ContentProperty("Instagram Url", "instagramUrl", false, "", 2, false)]
            public Textstring InstagramUrl { get; set; }

            [ContentProperty("Dribbble Url", "dribbbleUrl", false, "", 3, false)]
            public Textstring DribbbleUrl { get; set; }

        }
        public class Row1Tab : TabBase
        {
            [ContentProperty("Title1", "title1", false, "", 0, false)]
            public Textstring Title1 { get; set; }

            [ContentProperty("Description1", "description1", false, "", 1, false)]
            public TextboxMultiple Description1 { get; set; }

        }
        public class Row2Tab : TabBase
        {
            [ContentProperty("Title2", "title2", false, "", 0, false)]
            public Textstring Title2 { get; set; }

            [ContentProperty("Description2", "description2", false, "", 1, false)]
            public TextboxMultiple Description2 { get; set; }

            [ContentProperty("Image", "image", false, "", 2, false)]
            public Upload Image { get; set; }

        }
        public class Row3Tab : TabBase
        {
            [ContentProperty("Title3", "title3", false, "", 0, false)]
            public Textstring Title3 { get; set; }

            [ContentProperty("Description3", "description3", false, "", 1, false)]
            public TextboxMultiple Description3 { get; set; }

            [ContentProperty("image1", "image1", false, "", 2, false)]
            public Upload Image1 { get; set; }

        }
        public class Row4Tab : TabBase
        {
            [ContentProperty("Title4", "title4", false, "", 0, false)]
            public Textstring Title4 { get; set; }

            [ContentProperty("Description4", "description4", false, "", 1, false)]
            public TextboxMultiple Description4 { get; set; }

            [ContentProperty("image2", "image2", false, "", 2, false)]
            public Upload Image2 { get; set; }

        }
        public class Row5Tab : TabBase
        {
            [ContentProperty("Title5", "title5", false, "", 0, false)]
            public Textstring Title5 { get; set; }

            [ContentProperty("Description5", "description5", false, "", 1, false)]
            public TextboxMultiple Description5 { get; set; }

        }

        [ContentTab("Banner", 0)]
        public BannerTab Banner { get; set; }

        [ContentTab("Call to Action", 1)]
        public CallToActionTab CallToAction { get; set; }

        [ContentTab("Footer", 2)]
        public FooterTab Footer { get; set; }

        [ContentTab("Social", 3)]
        public SocialTab Social { get; set; }

        [ContentTab("Row1", 4)]
        public Row1Tab Row1 { get; set; }

        [ContentTab("Row2", 5)]
        public Row2Tab Row2 { get; set; }

        [ContentTab("Row3", 6)]
        public Row3Tab Row3 { get; set; }

        [ContentTab("Row4", 7)]
        public Row4Tab Row4 { get; set; }

        [ContentTab("Row5", 8)]
        public Row5Tab Row5 { get; set; }
    }
}