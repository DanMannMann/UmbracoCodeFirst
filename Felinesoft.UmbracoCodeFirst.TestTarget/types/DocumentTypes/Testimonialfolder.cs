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
    [DocumentType(@"Testimonial Folder", @"TestimonialFolder", new Type[] { typeof(Article), typeof(Testimonialfolder), typeof(Testimonial) }, @".sprTreeFolder", false, false, @"")]
    public class Testimonialfolder : Master
    {
    }
}