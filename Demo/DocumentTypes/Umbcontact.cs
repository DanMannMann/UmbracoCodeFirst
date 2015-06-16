
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace Demo.GeneratedUmbracoTypes
{
    [DocumentType(null, "Contact", "umbContact", null, ".sprTreeMember", false, false, false, "")]
    public class Umbcontact : DocumentTypeBase
    {
        public class ContentTab : TabBase
        {
            [DocumentProperty("Title", "title", "Umbraco.Textbox", "Textstring", null, false, "", 0, false)]
            public Textstring Title { get; set; }

            [DocumentProperty("Sub title", "subTitle", "Umbraco.Textbox", "Textstring", null, false, "", 1, false)]
            public Textstring SubTitle { get; set; }

            [DocumentProperty("Mail To Address", "umbEmailTo", "Umbraco.Textbox", "Textstring", null, false, "When someone fills out the contact form, where should their e-mail be sent?", 2, false)]
            public Textstring MailToAddress { get; set; }

        }

        [DocumentTab("Content", 0)]
        public ContentTab Content { get; set; }
    }
}