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
    [DocumentType(@"SEO Page", @"SeoPage", null, @"icon-document-dashed-line", true, false, @"")]
    [Template(false, "Article", "Article")]
    [Template(true, "Root", "Root")]
    public class Seopage : Master
    {
        public class SEOTab : TabBase
        {
            [ContentProperty(@"No Index", @"noIndex", true, @"", 0, false)]
            public TrueFalse Noindex { get; set; }

            [ContentProperty(@"Meta Title", @"metaTitle", true, @"", 2, false)]
            public Textstring Metatitle { get; set; }

            [ContentProperty(@"Meta Description", @"metaDescription", false, @"", 4, false)]
            public Textstring Metadescription { get; set; }

            [ContentProperty(@"Priority", @"priority", false, @"Please enter a number between 0.0 and 1.0", 5, false)]
            public Textstring Priority { get; set; }

            [ContentProperty(@"Optional Author", @"optionalAuthor", false, @"", 6, false)]
            public LegacyContentPicker Optionalauthor { get; set; }

            [ContentProperty(@"Change Frequency", @"changeFrequency", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.ChangeFrequency Changefrequency { get; set; }

            [ContentProperty(@"Canonical URL", @"canonicalUrl", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Canonicalurl { get; set; }

            [ContentProperty(@"Hide From Sitemap", @"hideFromSitemap", false, @"", 9, false)]
            public TrueFalse Hidefromsitemap { get; set; }

        }
        public class ReferencesTab : TabBase
        {
            [ContentProperty(@"Page References", @"pageReferences", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleTextBox Pagereferences { get; set; }

        }
        public class OpenGraphTab : TabBase
        {
            [ContentProperty(@"Open Graph Tags", @"openGraphTags", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.Union_Opengraphtags Opengraphtags { get; set; }

        }
        public class OnDemandTab : TabBase
        {
            [ContentProperty(@"Is On Demand Page", @"isOnDemandPage", false, @"", 0, false)]
            public TrueFalse Isondemandpage { get; set; }

            [ContentProperty(@"On Demand Login URL", @"onDemandLoginUrl", false, @"", 1, false)]
            public Textstring Ondemandloginurl { get; set; }

            [ContentProperty(@"Nav Override 1", @"navOverride1", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.NavOverridePicker Navoverride1 { get; set; }

            [ContentProperty(@"Nav Override 1 Name", @"navOverride1Name", false, @"", 4, false)]
            public Textstring Navoverride1name { get; set; }

            [ContentProperty(@"Nav Override 2", @"navOverride2", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.NavOverridePicker Navoverride2 { get; set; }

            [ContentProperty(@"Nav Override 2 Name", @"navOverride2Name", false, @"", 7, false)]
            public Textstring Navoverride2name { get; set; }

            [ContentProperty(@"Nav Override 3", @"navOverride3", false, @"", 9, false)]
            public LMI.BusinessLogic.CodeFirst.NavOverridePicker Navoverride3 { get; set; }

            [ContentProperty(@"Nav Override 3 Name", @"navOverride3Name", false, @"", 10, false)]
            public Textstring Navoverride3name { get; set; }

            [ContentProperty(@"Nav Override 1 Link", @"navOverride1Link", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Navoverride1link { get; set; }

            [ContentProperty(@"Nav Override 2 Link", @"navOverride2Link", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Navoverride2link { get; set; }

            [ContentProperty(@"Nav Override 3 Link", @"navOverride3Link", false, @"", 11, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Navoverride3link { get; set; }

            [ContentProperty(@"Sub Links  For This Page", @"subLinksForThisPage", false, @"Define the sub links shown when this page is navigated to", 2, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Sublinksforthispage { get; set; }

        }

        [ContentTab(@"SEO", 10)]
        public SEOTab SEO { get; set; }

        [ContentTab(@"References", 11)]
        public ReferencesTab References { get; set; }

        [ContentTab(@"Open Graph", 12)]
        public OpenGraphTab OpenGraph { get; set; }

        [ContentTab(@"On Demand", 13)]
        public OnDemandTab OnDemand { get; set; }

        [ContentProperty(@"Hide Redirect Dialog", "hideRedirectDialog", false, @"", 0, false)]
        public TrueFalse Hideredirectdialog { get; set; }

        [ContentProperty(@"Show Email Sign-Up", "showEmailSignUp", false, @"", 1, false)]
        public TrueFalse Showemailsignup { get; set; }
    }
}