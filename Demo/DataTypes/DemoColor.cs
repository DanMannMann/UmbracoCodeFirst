using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace Demo.DataTypes
{
    [DataType(BuiltInPropertyEditorAliases.ColorPicker, "Demo Approved Colors")]
    [PreValue("1", "111111")]
    [PreValue("2", "222222")]
    [PreValue("3", "333333")]
    public class DemoColor : ColorPicker
    {
    }
}