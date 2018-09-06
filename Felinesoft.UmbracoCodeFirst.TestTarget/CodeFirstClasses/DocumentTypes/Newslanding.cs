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
    [DocumentType(@"News Landing", @"NewsLanding", new Type[] { typeof(Articletopic), typeof(Redirectpage) }, @"icon-newspaper-alt", false, false, @"")]
    [Template(true, "News Landing", "NewsLanding")]
    public class Newslanding : Seopage
    {
        public class GridTab : TabBase
        {
            [ContentProperty(@"Grid Heading", @"gridHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Gridheading { get; set; }

            [ContentProperty(@"Grid Subheading", @"gridSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Gridsubheading { get; set; }

            [ContentProperty(@"Article 1", @"article1", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article1 { get; set; }

            [ContentProperty(@"Article 2", @"article2", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article2 { get; set; }

            [ContentProperty(@"Article 3", @"article3", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article3 { get; set; }

            [ContentProperty(@"Article 4", @"article4", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article4 { get; set; }

            [ContentProperty(@"Article 5", @"article5", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article5 { get; set; }

        }
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Most Popular Article 1", @"mostPopularArticle1", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Mostpopulararticle1 { get; set; }

            [ContentProperty(@"Most Popular Article 2", @"mostPopularArticle2", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Mostpopulararticle2 { get; set; }

            [ContentProperty(@"Most Popular Article 3", @"mostPopularArticle3", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Mostpopulararticle3 { get; set; }

            [ContentProperty(@"Most Popular Article 4", @"mostPopularArticle4", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Mostpopulararticle4 { get; set; }

            [ContentProperty(@"Most Popular Article 5", @"mostPopularArticle5", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Mostpopulararticle5 { get; set; }

            [ContentProperty(@"News By Topic Header", @"newsByTopicHeader", true, @"22 Character Limit", 1, false)]
            public Textstring Newsbytopicheader { get; set; }

            [ContentProperty(@"Most Popular Header", @"mostPopularHeader", true, @"22 Character Limit", 2, false)]
            public Textstring Mostpopularheader { get; set; }

            [ContentProperty(@"Article List Tags", @"articleListTags", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.TopicSelector Articlelisttags { get; set; }

            [ContentProperty(@"Browse Our Topics Header", @"browseOurTopicsHeader", true, @"", 0, false)]
            public Textstring Browseourtopicsheader { get; set; }

            [ContentProperty(@"Show All", @"showAll", true, @"Max Character limit: 28", 9, false)]
            public Textstring Showall { get; set; }

        }
        public class MenuTab : TabBase
        {
            [ContentProperty(@"Hide In Sub Nav", @"hideInSubNav", false, @"", 0, false)]
            public Checkbox Hideinsubnav { get; set; }

        }

        [ContentTab(@"Grid", 2)]
        public GridTab Grid { get; set; }

        [ContentTab(@"Content", 1)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Menu", 2)]
        public MenuTab Menu { get; set; }
    }
}