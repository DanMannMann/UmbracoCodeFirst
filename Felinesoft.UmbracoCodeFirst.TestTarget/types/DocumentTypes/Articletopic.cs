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

namespace UmbracoCodeFirst.GeneratedTypes
{
    [DocumentType(@"Article Topic", @"ArticleTopic", new Type[] { typeof(Article) }, @"icon-categories", false, false, @"")]
    [Template(true, "Topic Landing", "TopicLanding")]
    public class Articletopic : Seopage
    {
        public class GridTab : TabBase
        {
            [ContentProperty(@"Grid Heading", @"gridHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Gridheading { get; set; }

            [ContentProperty(@"Grid Subheading", @"gridSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Gridsubheading { get; set; }

            [ContentProperty(@"Article 1", @"article1", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article1 { get; set; }

            [ContentProperty(@"Article 2", @"article2", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article2 { get; set; }

            [ContentProperty(@"Article 3", @"article3", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article3 { get; set; }

            [ContentProperty(@"Article 4", @"article4", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article4 { get; set; }

            [ContentProperty(@"Article 5", @"article5", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article5 { get; set; }

        }
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Most Popular Article 1", @"mostPopularArticle1", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Mostpopulararticle1 { get; set; }

            [ContentProperty(@"Most Popular Article 2", @"mostPopularArticle2", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Mostpopulararticle2 { get; set; }

            [ContentProperty(@"Most Popular Article 3", @"mostPopularArticle3", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Mostpopulararticle3 { get; set; }

            [ContentProperty(@"Most Popular Article 4", @"mostPopularArticle4", false, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Mostpopulararticle4 { get; set; }

            [ContentProperty(@"Most Popular Article 5", @"mostPopularArticle5", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Mostpopulararticle5 { get; set; }

            [ContentProperty(@"Most Popular Header", @"mostPopularHeader", true, @"22 Characters Max", 3, false)]
            public Textstring Mostpopularheader { get; set; }

            [ContentProperty(@"News By Topic Header", @"newsByTopicHeader", true, @"22 Characters Max", 0, false)]
            public Textstring Newsbytopicheader { get; set; }

            [ContentProperty(@"Article List Tags", @"articleListTags", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.TopicSelector Articlelisttags { get; set; }

            [ContentProperty(@"Social Subheader", @"socialSubheader", true, @"(Title on News Landing page)", 1, false)]
            public Textstring Socialsubheader { get; set; }

        }
        public class SEOTab : TabBase
        {
        }
        public class MenuTab : TabBase
        {
            [ContentProperty(@"Show in Flyout Menu", @"showInFlyoutMenu", false, @"", 0, false)]
            public TrueFalse Showinflyoutmenu { get; set; }

        }

        [ContentTab(@"Grid", 2)]
        public GridTab Grid { get; set; }

        [ContentTab(@"Content", 1)]
        public ContentTab Content { get; set; }

        [ContentTab(@"SEO", 0)]
        public SEOTab SEO { get; set; }

        [ContentTab(@"Menu", 3)]
        public MenuTab Menu { get; set; }
    }
}