using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"Settings", @"Settings", new Type[] { typeof(Settings), typeof(Culturesettings) }, @"icon-umb-developer", false, false, @"")]
    public class Settings : Master
    {
        public class ConfigTab : TabBase
        {
            [ContentProperty(@"Topics", @"topics", false, @"This is the list of topics you can associate with an article.", 0, false)]
            public LMI.BusinessLogic.CodeFirst.Topics Topics { get; set; }

            [ContentProperty(@"Pick Search Page", @"pickSearchPage", false, @"", 2, false)]
            public LegacyContentPicker Picksearchpage { get; set; }

            [ContentProperty(@"Event Types", @"eventTypes", false, @"", 1, false)]
            public Tags Eventtypes { get; set; }

            [ContentProperty(@"Site is Global Template", @"siteIsGlobalTemplate", false, @"Tick this box if the site is a global template site ", 4, false)]
            public Checkbox Siteisglobaltemplate { get; set; }

            [ContentProperty(@"Event List Default Row Number", @"eventListDefaultRowNumber", false, @"Set up the number of row on event listing to display at once", 5, false)]
            public Numeric Eventlistdefaultrownumber { get; set; }

            [ContentProperty(@"Event By Type List Item Number", @"eventByTypeListItemNumber", false, @"display item number of event by type on side bar", 6, false)]
            public Numeric Eventbytypelistitemnumber { get; set; }

        }
        public class SocialTab : TabBase
        {
            [ContentProperty(@"Twitter", @"twitter", false, @"", 1, false)]
            public Textstring Twitter { get; set; }

            [ContentProperty(@"Facebook", @"facebook", false, @"", 4, false)]
            public Textstring Facebook { get; set; }

            [ContentProperty(@"Instagram", @"instagram", false, @"", 6, false)]
            public Textstring Instagram { get; set; }

            [ContentProperty(@"Pinterest", @"pinterest", false, @"", 7, false)]
            public Textstring Pinterest { get; set; }

            [ContentProperty(@"Youtube", @"youtube", false, @"", 8, false)]
            public Textstring Youtube { get; set; }

            [ContentProperty(@"Linked In", @"linkedIn", false, @"", 2, false)]
            public Textstring Linkedin { get; set; }

            [ContentProperty(@"Google Plus URL", @"googlePlusUrl", false, @"", 3, false)]
            public Textstring Googleplusurl { get; set; }

            [ContentProperty(@"Email", @"email", false, @"", 0, false)]
            public Textstring Email { get; set; }

            [ContentProperty(@"Facebook Id", @"facebookId", false, @"", 9, false)]
            public Textstring Facebookid { get; set; }

        }
        public class MenuTab : TabBase
        {
            [ContentProperty(@"News Landing Page", @"newsLandingPage", false, @"", 4, false)]
            public LegacyContentPicker Newslandingpage { get; set; }

            [ContentProperty(@"Classes Landing Page", @"classesLandingPage", false, @"", 0, false)]
            public LegacyContentPicker Classeslandingpage { get; set; }

            [ContentProperty(@"Instructors Landing Page", @"instructorsLandingPage", false, @"", 3, false)]
            public LegacyContentPicker Instructorslandingpage { get; set; }

            [ContentProperty(@"Club Landing Page", @"clubLandingPage", false, @"", 2, false)]
            public LegacyContentPicker Clublandingpage { get; set; }

            [ContentProperty(@"Community Landing Page", @"communityLandingPage", false, @"", 5, false)]
            public LegacyContentPicker Communitylandingpage { get; set; }

            [ContentProperty(@"Shop Landing Page", @"shopLandingPage", false, @"", 6, false)]
            public LegacyContentPicker Shoplandingpage { get; set; }

            [ContentProperty(@"On Demand Page", @"onDemandPage", false, @"", 1, false)]
            public LegacyContentPicker Ondemandpage { get; set; }

        }
        public class MetadataTab : TabBase
        {
            [ContentProperty(@"Title Prefix", @"titlePrefix", false, @"", 0, false)]
            public Textstring Titleprefix { get; set; }

            [ContentProperty(@"Title Sufix", @"titleSufix", false, @"", 1, false)]
            public Textstring Titlesufix { get; set; }

        }
        public class FooterTab : TabBase
        {
            [ContentProperty(@"Primary Footer Category 1 Name", @"primaryFooterCategory1Name", false, @"", 3, false)]
            public Textstring Primaryfootercategory1name { get; set; }

            [ContentProperty(@"Footer Category 1 Links", @"footerCategory1Links", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Footercategory1links { get; set; }

            [ContentProperty(@"Primary Footer Category 2 Name", @"primaryFooterCategory2Name", false, @"", 5, false)]
            public Textstring Primaryfootercategory2name { get; set; }

            [ContentProperty(@"Footer Category 2 Links", @"footerCategory2Links", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Footercategory2links { get; set; }

            [ContentProperty(@"Footer Category 2 Links 2", @"footerCategory2Links2", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Footercategory2links2 { get; set; }

            [ContentProperty(@"Footer Category 2 Links 3", @"footerCategory2Links3", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Footercategory2links3 { get; set; }

            [ContentProperty(@"Footer Strapline", @"footerStrapline", false, @"", 1, false)]
            public Textstring Footerstrapline { get; set; }

            [ContentProperty(@"Footer Text", @"footerText", false, @"Maximum 170 Characters", 2, false)]
            public TextboxMultiple Footertext { get; set; }

            [ContentProperty(@"Footer Copyright Text", @"footerCopyrightText", false, @"55 Characters Max", 9, false)]
            public Textstring Footercopyrighttext { get; set; }

            [ContentProperty(@"Secondary Links", @"secondaryLinks", false, @"", 10, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Secondarylinks { get; set; }

            [ContentProperty(@"Footer Logo", @"footerLogo", false, @"", 0, false)]
            public LegacyMediaPicker Footerlogo { get; set; }

            [ContentProperty(@"Footer Yellow Custom Link Text", @"footerYellowCustomLinkText", false, @"", 11, false)]
            public Textstring Footeryellowcustomlinktext { get; set; }

            [ContentProperty(@"Footer Yellow Custom Link Url", @"footerYellowCustomLinkUrl", false, @"", 12, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Footeryellowcustomlinkurl { get; set; }

            [ContentProperty(@"Form Footer", @"formFooter", false, @"", 13, false)]
            public RichtextEditor Formfooter { get; set; }

        }
        public class FindAClassTab : TabBase
        {
            [ContentProperty(@"Find a Class Title", @"findAClassTitle", true, @"", 0, false)]
            public Textstring Findaclasstitle { get; set; }

            [ContentProperty(@"Find a Class Subtitle", @"findAClassSubtitle", true, @"Put { } around the part of the sub title you want to be a link. e.g. My {link} is here between the braces.", 1, false)]
            public Textstring Findaclasssubtitle { get; set; }

            [ContentProperty(@"Find a Class Subtitle Link", @"findAClassSubtitleLink", true, @"The link that will surround the text enclosed in { } ", 2, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Findaclasssubtitlelink { get; set; }

        }

        [ContentTab(@"Config", 1)]
        public ConfigTab Config { get; set; }

        [ContentTab(@"Social", 3)]
        public SocialTab Social { get; set; }

        [ContentTab(@"Menu", 2)]
        public MenuTab Menu { get; set; }

        [ContentTab(@"Metadata", 3)]
        public MetadataTab Metadata { get; set; }

        [ContentTab(@"Footer", 4)]
        public FooterTab Footer { get; set; }

        [ContentTab(@"Find a Class", 5)]
        public FindAClassTab FindAClass { get; set; }
    }
}