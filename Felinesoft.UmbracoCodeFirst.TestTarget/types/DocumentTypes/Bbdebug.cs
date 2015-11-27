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
    [DocumentType(@"BBDebug", @"Bbdebug", null, @".sprTreeFolder", false, false, @"")]
    public class Bbdebug : Master
    {

        [ContentProperty(@"Form", "formForm", false, @"", 0, false)]
        public UmbracoCodeFirst.GeneratedTypes.FormPicker Formform { get; set; }
    }
}