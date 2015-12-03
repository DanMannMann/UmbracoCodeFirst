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
    [DocumentType(@"MODULE: Testimonial", @"MODULETestimonial", null, @".sprTreeFolder", false, false, @"")]
    public class Moduletestimonial : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"testimonialsModuleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Testimonialsmoduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"testimonalsModuleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Testimonalsmodulesubheading { get; set; }

            [ContentProperty(@"Testimonial 1", @"testimonial1", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.TestimonialsPicker Testimonial1 { get; set; }

            [ContentProperty(@"Testimonial 2", @"testimonial2", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.TestimonialsPicker Testimonial2 { get; set; }

            [ContentProperty(@"Testimonial 3", @"testimonial3", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.TestimonialsPicker Testimonial3 { get; set; }

            [ContentProperty(@"Testimonial 4", @"testimonial4", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.TestimonialsPicker Testimonial4 { get; set; }

            [ContentProperty(@"Testimonial 5", @"testimonial5", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.TestimonialsPicker Testimonial5 { get; set; }

            [ContentProperty(@"Leave Testimonial Header", @"leaveTestimonialHeader", false, @"Max Character limit: 52", 7, false)]
            public Textstring Leavetestimonialheader { get; set; }

            [ContentProperty(@"Leave Testimonial Background Colour", @"leaveTestimonialBackgroundColour", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Leavetestimonialbackgroundcolour { get; set; }

            [ContentProperty(@"Leave Testimonial Button Text", @"leaveTestimonialButtonText", false, @"Max Character limit: 22", 9, false)]
            public Textstring Leavetestimonialbuttontext { get; set; }

            [ContentProperty(@"Leave Testimonial Button Url", @"leaveTestimonialButtonUrl", false, @"", 10, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Leavetestimonialbuttonurl { get; set; }

            [ContentProperty(@"More Testimonials Header", @"moreTestimonialsHeader", false, @"Shown only if there is room", 11, false)]
            public Textstring Moretestimonialsheader { get; set; }

            [ContentProperty(@"More Testimonials CTA Text", @"moreTestimonialsCtaText", false, @"Shown only if there is room", 12, false)]
            public Textstring Moretestimonialsctatext { get; set; }

            [ContentProperty(@"More Testimonials CTA URL", @"moreTestimonialsCtaUrl", false, @"", 13, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Moretestimonialsctaurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}