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
    [DocumentType(@"Region", @"Region", new Type[] { typeof(Homepage), typeof(Webpage), typeof(Redirecthomepage), typeof(Culture) }, @"icon-globe", false, false, @"")]
    public class Region : Master
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Dictionary Name", @"dictionaryName", true, @"Please copy / paste name of dictionary item containing name of this node.", 0, false)]
            public Textstring Dictionaryname { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}