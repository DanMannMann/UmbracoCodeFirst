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
    [DocumentType(@"MODULE: Join the Tribe", @"MODULEJoinTheTribe", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULE Join The Tribe", "MODULEJoinTheTribe")]
    public class Modulejointhetribe : Modules
    {
        public class SocialBlockTab : TabBase
        {
            [ContentProperty(@"Social Media Service", @"socialMediaService", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SocialMediaPicker Socialmediaservice { get; set; }

            [ContentProperty(@"Twitter Username", @"twitterUsername", false, @"Max Character limit: 15", 2, false)]
            public Textstring Twitterusername { get; set; }

            [ContentProperty(@"Twitter Post", @"twitterPost", false, @"Max Character limit: 140", 3, false)]
            public Textstring Twitterpost { get; set; }

            [ContentProperty(@"Social Block", @"imageLabelBox2", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box2 Imagelabelbox2 { get; set; }

            [ContentProperty(@"Facebook Post", @"facebookPost", false, @"Max Character limit: 164", 4, false)]
            public Textstring Facebookpost { get; set; }

            [ContentProperty(@"Facebook Url", @"facebookUrl", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Facebookurl { get; set; }

        }
        public class SingleBigBlockTab : TabBase
        {
            [ContentProperty(@"Type", @"bigBlockType", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePromoPicker Bigblocktype { get; set; }

            [ContentProperty(@"Article", @"bigBlockArticle", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Bigblockarticle { get; set; }

            [ContentProperty(@"Promo Image", @"bigBlockPromoImage", false, @"", 4, false)]
            public LegacyMediaPicker Bigblockpromoimage { get; set; }

            [ContentProperty(@"Promo Text", @"bigBlockPromoText", false, @"", 7, false)]
            public Textstring Bigblockpromotext { get; set; }

            [ContentProperty(@"Promo Date", @"bigBlockPromoDate", false, @"", 6, false)]
            public DatePicker Bigblockpromodate { get; set; }

            [ContentProperty(@"Promo Url", @"bigBlockPromoUrl", false, @"", 9, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Bigblockpromourl { get; set; }

            [ContentProperty(@"Promo", @"bigBlockPromoLabel", false, @"", 3, false)]
            public Label Bigblockpromolabel { get; set; }

            [ContentProperty(@"Promo CTA", @"bigBlockPromoCTA", false, @"", 8, false)]
            public Textstring Bigblockpromocta { get; set; }

            [ContentProperty(@"Big Block", @"imageLabelBox3", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box3 Imagelabelbox3 { get; set; }

            [ContentProperty(@"Background Colour", @"singleBigBlockBackgroundColour", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Singlebigblockbackgroundcolour { get; set; }

        }
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"moduleHeading", false, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Social strapline", @"socialStrapline", true, @"Max Character limit: 64", 2, false)]
            public Textstring Socialstrapline { get; set; }

        }
        public class StatsPromoBlocksTab : TabBase
        {
            [ContentProperty(@"Background Color", @"statBlockBGColour1", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Statblockbgcolour1 { get; set; }

            [ContentProperty(@"Background Color", @"statBlockBGColour2", false, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Statblockbgcolour2 { get; set; }

            [ContentProperty(@"Background Color", @"statBlockBGColour3", false, @"", 11, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Statblockbgcolour3 { get; set; }

            [ContentProperty(@"Background Color", @"statBlockBGColour4", false, @"", 15, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Statblockbgcolour4 { get; set; }

            [ContentProperty(@"Heading", @"statBlockHeading1", false, @"Max Character limit: 11", 1, false)]
            public Textstring Statblockheading1 { get; set; }

            [ContentProperty(@"Subheading", @"statBlockSubheading1", false, @"Max Character limit: 27", 2, false)]
            public Textstring Statblocksubheading1 { get; set; }

            [ContentProperty(@"Heading", @"statBlockHeading2", false, @"Max Character limit: 11", 5, false)]
            public Textstring Statblockheading2 { get; set; }

            [ContentProperty(@"Heading", @"statBlockHeading3", false, @"Max Character limit: 11", 9, false)]
            public Textstring Statblockheading3 { get; set; }

            [ContentProperty(@"Heading", @"statBlockHeading4", false, @"Max Character limit: 11", 13, false)]
            public Textstring Statblockheading4 { get; set; }

            [ContentProperty(@"Subheading", @"statBlockSubheading2", false, @"Max Character limit: 27", 6, false)]
            public Textstring Statblocksubheading2 { get; set; }

            [ContentProperty(@"Subheading", @"statBlockSubheading3", false, @"Max Character limit: 27", 10, false)]
            public Textstring Statblocksubheading3 { get; set; }

            [ContentProperty(@"Subheading", @"statBlockSubheading4", false, @"Max Character limit: 27", 14, false)]
            public Textstring Statblocksubheading4 { get; set; }

            [ContentProperty(@"Stats Block 1", @"imageLabelBox4", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box4 Imagelabelbox4 { get; set; }

            [ContentProperty(@"Stats Block 2", @"imageLabelBox5", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box5 Imagelabelbox5 { get; set; }

            [ContentProperty(@"Stats Block 4", @"imageLabelBox8", false, @"", 12, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box8 Imagelabelbox8 { get; set; }

            [ContentProperty(@"Stats Block 4", @"imageLabelBox7", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box7 Imagelabelbox7 { get; set; }

        }
        public class ArticlePromoBlocksTab : TabBase
        {
            [ContentProperty(@"Article", @"article1", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article1 { get; set; }

            [ContentProperty(@"Text Color", @"articleTextColour1", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.BlackOrWhite Articletextcolour1 { get; set; }

            [ContentProperty(@"Background Color", @"articleBackgroundColour1", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Articlebackgroundcolour1 { get; set; }

            [ContentProperty(@"Article Block 1", @"articleBlock1", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box1 Articleblock1 { get; set; }

            [ContentProperty(@"Article Block 2", @"articleBlock2", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box6 Articleblock2 { get; set; }

            [ContentProperty(@"Article", @"article2", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article2 { get; set; }

            [ContentProperty(@"Text Color", @"articleTextColour2", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.BlackOrWhite Articletextcolour2 { get; set; }

            [ContentProperty(@"Background Color", @"articleBackgroundColour2", false, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Articlebackgroundcolour2 { get; set; }

            [ContentProperty(@"Article Block 3", @"articleBlock3", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.JoinTheTribe_Box9 Articleblock3 { get; set; }

            [ContentProperty(@"Article", @"article3", false, @"", 9, false)]
            public UmbracoCodeFirst.GeneratedTypes.ArticlePicker Article3 { get; set; }

            [ContentProperty(@"Text Color", @"articleTextColour3", false, @"", 10, false)]
            public UmbracoCodeFirst.GeneratedTypes.BlackOrWhite Articletextcolour3 { get; set; }

            [ContentProperty(@"Background Color", @"articleBackgroundColour3", false, @"", 11, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Articlebackgroundcolour3 { get; set; }

        }

        [ContentTab(@"Social Block", 1)]
        public SocialBlockTab SocialBlock { get; set; }

        [ContentTab(@"Single Big Block", 2)]
        public SingleBigBlockTab SingleBigBlock { get; set; }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Stats Promo Blocks", 3)]
        public StatsPromoBlocksTab StatsPromoBlocks { get; set; }

        [ContentTab(@"Article Promo Blocks", 4)]
        public ArticlePromoBlocksTab ArticlePromoBlocks { get; set; }
    }
}