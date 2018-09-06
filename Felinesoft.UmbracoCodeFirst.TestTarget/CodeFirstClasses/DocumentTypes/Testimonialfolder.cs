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
    [DocumentType(@"Testimonial Folder", @"TestimonialFolder", new Type[] { typeof(Article), typeof(Testimonialfolder), typeof(Testimonial) }, @".sprTreeFolder", false, false, @"")]
    public class Testimonialfolder : Master
    {
    }
}