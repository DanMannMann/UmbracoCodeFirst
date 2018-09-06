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
    [DocumentType(@"BBDebug", @"Bbdebug", null, @".sprTreeFolder", false, false, @"")]
    public class Bbdebug : Master
    {

        [ContentProperty(@"Form", "formForm", false, @"", 0, false)]
        public LMI.BusinessLogic.CodeFirst.FormPicker Formform { get; set; }
    }
}