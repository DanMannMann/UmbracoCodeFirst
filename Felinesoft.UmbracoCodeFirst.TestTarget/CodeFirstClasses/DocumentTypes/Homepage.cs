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

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"Home Page", @"HomePage", new Type[] { typeof(Modules), typeof(Settings), typeof(Classeslanding), typeof(Region), typeof(Search), typeof(Newslanding), typeof(Testimonialfolder), typeof(Sitemap), typeof(Webpage), typeof(Instructorslanding), typeof(Rootsitemap), typeof(Clubs), typeof(Services), typeof(Redirectpage), typeof(Archivefolder), typeof(Bbdebug), typeof(Debug), typeof(Errorpage), typeof(Events), typeof(Sitemapindex) }, @"icon-home", true, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Homepage : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Dictionary Name", @"dictionaryName", true, @"Please copy & paste the dictionary alias for this site's title", 0, false)]
            public Textstring Dictionaryname { get; set; }

            [ContentProperty(@"Hero Background colour", @"heroBackgroundColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Herobackgroundcolour { get; set; }

            [ContentProperty(@"Hero Footer Heading", @"heroFooterHeading", false, @"", 2, false)]
            public Textstring Herofooterheading { get; set; }

            [ContentProperty(@"Hero Footer Subheading", @"heroFooterSubheading", false, @"", 3, false)]
            public Textstring Herofootersubheading { get; set; }

        }
        public class ModuleTeaser1Tab : TabBase
        {
            [ContentProperty(@"Button Url", @"buttonUrl_tab1", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl_Tab1 { get; set; }

            [ContentProperty(@"Module heading", @"moduleHeading_tab1", false, @"Max Character limit: 27", 6, false)]
            public Textstring Moduleheading_Tab1 { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading_tab1", false, @"", 5, false)]
            public Textstring Modulesubheading_Tab1 { get; set; }

            [ContentProperty(@"Hero Heading", @"heroHeading_tab1", false, @"Max Character limit: 15", 1, false)]
            public Textstring Heroheading_Tab1 { get; set; }

            [ContentProperty(@"Hero subheading", @"heroSubheading_tab1", false, @"Max Character limit: 47", 2, false)]
            public Textstring Herosubheading_Tab1 { get; set; }

            [ContentProperty(@"Button Text", @"buttonText_tab1", false, @"Max Character limit: 22", 3, false)]
            public Textstring Buttontext_Tab1 { get; set; }

            [ContentProperty(@"Body text", @"bodyText_tab1", false, @"Max Character limit: 133", 7, false)]
            public Textstring Bodytext_Tab1 { get; set; }

            [ContentProperty(@"Module Image", @"moduleImage_Tab1", false, @"", 0, false)]
            public LegacyMediaPicker Moduleimage_Tab1 { get; set; }

            [ContentProperty(@"Display Module", @"moduleDisplay_Tab1", false, @"Please check this if you would like this module to display.", 8, false)]
            public TrueFalse Moduledisplay_Tab1 { get; set; }

            [ContentProperty(@"Module Video", @"moduleVideo_Tab1", false, @"", 9, false)]
            public LegacyMediaPicker Modulevideo_Tab1 { get; set; }

        }
        public class ModuleTeaser2Tab : TabBase
        {
            [ContentProperty(@"Hero Heading", @"heroHeading_tab2", false, @"Max Character limit: 15", 1, false)]
            public Textstring Heroheading_Tab2 { get; set; }

            [ContentProperty(@"Hero subheading", @"heroSubheading_tab2", false, @"Max Character limit: 47", 2, false)]
            public Textstring Herosubheading_Tab2 { get; set; }

            [ContentProperty(@"Module Image", @"moduleImage_Tab2", false, @"", 0, false)]
            public LegacyMediaPicker Moduleimage_Tab2 { get; set; }

            [ContentProperty(@"Body text", @"bodyText_tab2", false, @"Max Character limit: 133", 7, false)]
            public Textstring Bodytext_Tab2 { get; set; }

            [ContentProperty(@"Button Text", @"buttonText_tab2", false, @"Max Character limit: 22", 3, false)]
            public Textstring Buttontext_Tab2 { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl_tab2", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl_Tab2 { get; set; }

            [ContentProperty(@"Module heading", @"moduleHeading_tab2", false, @"Max Character limit: 27", 6, false)]
            public Textstring Moduleheading_Tab2 { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading_tab2", false, @"", 5, false)]
            public Textstring Modulesubheading_Tab2 { get; set; }

            [ContentProperty(@"Display Module", @"moduleDisplay_Tab2", false, @"Please check this if you would like this module to display.", 8, false)]
            public TrueFalse Moduledisplay_Tab2 { get; set; }

            [ContentProperty(@"Module Video", @"moduleVideo_Tab2", false, @"", 9, false)]
            public LegacyMediaPicker Modulevideo_Tab2 { get; set; }

        }
        public class ModuleTeaser3Tab : TabBase
        {
            [ContentProperty(@"Module Image", @"moduleImage_Tab3", false, @"", 0, false)]
            public LegacyMediaPicker Moduleimage_Tab3 { get; set; }

            [ContentProperty(@"Hero Heading", @"heroHeading_tab3", false, @"Max Character limit: 15", 1, false)]
            public Textstring Heroheading_Tab3 { get; set; }

            [ContentProperty(@"Hero subheading", @"heroSubheading_tab3", false, @"Max Character limit: 47", 2, false)]
            public Textstring Herosubheading_Tab3 { get; set; }

            [ContentProperty(@"Body text", @"bodyText_tab3", false, @"Max Character limit: 133", 7, false)]
            public Textstring Bodytext_Tab3 { get; set; }

            [ContentProperty(@"Button Text", @"buttonText_tab3", false, @"Max Character limit: 22", 3, false)]
            public Textstring Buttontext_Tab3 { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl_tab3", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl_Tab3 { get; set; }

            [ContentProperty(@"Module heading", @"moduleHeading_tab3", false, @"Max Character limit: 27", 6, false)]
            public Textstring Moduleheading_Tab3 { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading_tab3", false, @"", 5, false)]
            public Textstring Modulesubheading_Tab3 { get; set; }

            [ContentProperty(@"Display Module", @"moduleDisplay_Tab3", false, @"Please check this if you would like this module to display.", 8, false)]
            public TrueFalse Moduledisplay_Tab3 { get; set; }

            [ContentProperty(@"Module Video", @"moduleVideo_Tab3", false, @"", 9, false)]
            public LegacyMediaPicker Modulevideo_Tab3 { get; set; }

        }
        public class RedirectPopupTab : TabBase
        {
        }
        public class SEOTab : TabBase
        {
            [ContentProperty(@"Allow Site To Be Indexed", @"allowSiteToBeIndexed", false, @"Allow site to be indexed by Google.", 0, false)]
            public TrueFalse Allowsitetobeindexed { get; set; }

        }
        public class CookiesTab : TabBase
        {
            [ContentProperty(@"Enable Cookies Warning", @"enableCookiesWarning", false, @"", 0, false)]
            public TrueFalse Enablecookieswarning { get; set; }

            [ContentProperty(@"Privacy Policy", @"privacyPolicy", false, @"To insert the Privacy Policy Url into the text below please use the {pp} syntax", 1, false)]
            public RichtextEditor Privacypolicy { get; set; }

            [ContentProperty(@"Confirmation Text", @"privacyPolicyAccept", true, @"", 2, false)]
            public Textstring Privacypolicyaccept { get; set; }

        }

        [ContentTab(@"Content", 1)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Module  Teaser 1", 2)]
        public ModuleTeaser1Tab ModuleTeaser1 { get; set; }

        [ContentTab(@"Module  Teaser 2", 3)]
        public ModuleTeaser2Tab ModuleTeaser2 { get; set; }

        [ContentTab(@"Module  Teaser 3", 4)]
        public ModuleTeaser3Tab ModuleTeaser3 { get; set; }

        [ContentTab(@"Redirect Popup", 4)]
        public RedirectPopupTab RedirectPopup { get; set; }

        [ContentTab(@"SEO", 0)]
        public SEOTab SEO { get; set; }

        [ContentTab(@"Cookies", 6)]
        public CookiesTab Cookies { get; set; }

        [ContentProperty(@"HREF Language Value", "hrefLanguageValue", false, @"e.g. x-default", 0, false)]
        public Textstring Hreflanguagevalue { get; set; }

        [ContentProperty(@"Is Template Site", "isTemplateSite", false, @"", 1, false)]
        public TrueFalse Istemplatesite { get; set; }
    }
}