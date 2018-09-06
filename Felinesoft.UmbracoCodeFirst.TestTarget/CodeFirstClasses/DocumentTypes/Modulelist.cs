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
    [DocumentType(@"MODULE: List", @"MODULEList", new Type[] { typeof(Submodulelistitem) }, @".sprTreeFolder", false, false, @"")]
    public class Modulelist : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module header", @"moduleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Display Images", @"displayImages", false, @"", 2, false)]
            public Checkbox Displayimages { get; set; }

            [ContentProperty(@"List Image", @"listImage", false, @"Please provide an image for the sidebar if the list is set not to show images.", 3, false)]
            public LegacyMediaPicker Listimage { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}