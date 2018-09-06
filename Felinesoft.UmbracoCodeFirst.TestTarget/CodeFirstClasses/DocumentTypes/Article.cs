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
    [DocumentType(@"Article", @"Article", null, @"icon-notepad-alt", false, false, @"")]
    [Template(true, "Article", "Article")]
    [Template(false, "Debug", "Debug")]
    public class Article : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Heading", @"heading", true, @"Max Character limit: 61 ", 3, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Snippet", @"snippet", true, @"Max Character limit: 357", 4, false)]
            public TextboxMultiple Snippet { get; set; }

            [ContentProperty(@"Body", @"body", false, @"", 5, false)]
            public RichtextEditor Body { get; set; }

            [ContentProperty(@"Article Title", @"articleTitle", true, @"Max Character limit: 36", 0, false)]
            public Textstring Articletitle { get; set; }

            [ContentProperty(@"Article Snippet", @"articleSnippet", false, @"Max Character limit: 75", 2, false)]
            public Textstring Articlesnippet { get; set; }

            [ContentProperty(@"Topics", @"topics", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.TopicSelector_Limit_4_ Topics { get; set; }

            [ContentProperty(@"Email Form Heading", @"emailFormHeading", false, @"", 7, false)]
            public Textstring Emailformheading { get; set; }

            [ContentProperty(@"Email Form SubHeading", @"emailFormSubheading", false, @"", 8, false)]
            public Textstring Emailformsubheading { get; set; }

        }
        public class GalleryTab : TabBase
        {
            [ContentProperty(@"Images", @"images", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleMediaPicker Images { get; set; }

        }
        public class MetadataTab : TabBase
        {
        }
        public class MoreNewsTab : TabBase
        {
            [ContentProperty(@"More News Article 1", @"moreNewsArticle1", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Morenewsarticle1 { get; set; }

            [ContentProperty(@"More News Article 2", @"moreNewsArticle2", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Morenewsarticle2 { get; set; }

            [ContentProperty(@"More News Article 3", @"moreNewsArticle3", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Morenewsarticle3 { get; set; }

            [ContentProperty(@"More News Article 4", @"moreNewsArticle4", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Morenewsarticle4 { get; set; }

            [ContentProperty(@"More News Header", @"moreNewsHeader", true, @"Max character length 25", 0, false)]
            public Textstring Morenewsheader { get; set; }

            [ContentProperty(@"More News Sub Header", @"moreNewsSubHeader", false, @"Max character length 65", 1, false)]
            public Textstring Morenewssubheader { get; set; }

            [ContentProperty(@"MoreNewsCTAText", @"moreNewsCtaText", true, @"", 6, false)]
            public Textstring Morenewsctatext { get; set; }

            [ContentProperty(@"More News CTA Url", @"moreNewsCtaUrl", true, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Morenewsctaurl { get; set; }

        }
        public class ImagesTab : TabBase
        {
            [ContentProperty(@"Image", @"image", false, @"", 0, false)]
            public LegacyMediaPicker Image { get; set; }

            [ContentProperty(@"Grid Image", @"gridImage", false, @"", 1, false)]
            public LegacyMediaPicker Gridimage { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Gallery", 1)]
        public GalleryTab Gallery { get; set; }

        [ContentTab(@"Metadata", 2)]
        public MetadataTab Metadata { get; set; }

        [ContentTab(@"More News", 2)]
        public MoreNewsTab MoreNews { get; set; }

        [ContentTab(@"Images", 3)]
        public ImagesTab Images { get; set; }

        [ContentProperty(@"Author Override", "authorOverride", false, @"", 1, false)]
        public LMI.BusinessLogic.CodeFirst.AuthorPicker Authoroverride { get; set; }

        [ContentProperty(@"Date Override", "dateOverride", false, @"", 0, false)]
        public DatePickerWithTime Dateoverride { get; set; }
    }
}