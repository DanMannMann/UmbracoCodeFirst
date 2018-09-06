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
    [DocumentType(@"Repository Locale", @"RepositoryLocale", null, @"icon-company", false, false, @"")]
    public class Repositorylocale : Localisationfolder
    {
        public class TimeagoSettingsTab : TabBase
        {
            [ContentProperty(@"Js file Name", @"jsFileName", true, @"The time ago js file name to load for localisation", 0, false)]
            public Textstring Jsfilename { get; set; }

            [ContentProperty(@"Culture Codes", @"cultureCodes", true, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleTextBox Culturecodes { get; set; }

        }

        [ContentTab(@"TimeAgo Settings", 0)]
        public TimeagoSettingsTab TimeagoSettings { get; set; }
    }
}