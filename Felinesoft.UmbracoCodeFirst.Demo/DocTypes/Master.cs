
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
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.Linq;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    [DocumentType(icon:BuiltInIcons.SprTreeFolder)]
    [Template(isDefault: true)]
    public class Master : DocumentTypeBase
    {
        public class SEOTab : TabBase
        {
            [ContentProperty("Meta Keywords", "metaKeywords", false, "", 0, false)]
            public virtual Textstring MetaKeywords { get; set; }

            [ContentProperty("Meta Description", "metaDescription", false, "", 1, false)]
            public virtual Textstring MetaDescription { get; set; }
        }

        [ContentTab]
        public virtual SEOTab SEO { get; set; }

        [ContentProperty("Hide in navigation?", "umbracoNaviHide", false, "If set to \"Yes\", this page will be hidden from the navigation menu.", 0, false)]
        public virtual TrueFalse HideInNavigation { get; set; }

        public virtual IEnumerable<NavigationItem> NavigationItems
        {
            get
            {
                Home home;
                try
                {
                    home = this.NearestAncestor<Home>();
                }
                catch
                {
                    return new List<NavigationItem>();
                }
                var lst = home.NodeDetails.PublishedContent.Children.Where("Visible").Select(x => new NavigationItem() { Url = x.Url, Name = x.Name }).ToList();
                lst.Insert(0, new NavigationItem() { Url = "/", Name = "Home" });
                return lst;
            }
        }

        public string SiteName
        {
            get
            {
                Home home;
                try
                {
                    home = this.NearestAncestor<Home>();
                }
                catch
                {
                    return string.Empty;
                }

                return home.Banner.SiteName.Value;
            }
        }
    }

    public class NavigationItem
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }
}