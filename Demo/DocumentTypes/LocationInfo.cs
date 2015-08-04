using Demo.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;

namespace Demo.DocumentTypes
{
    [DocumentType]
    public class LocationInfo : DocumentTypeBase
    {
        [DocumentProperty]
        public string Name { get; set; }

        [DocumentProperty]
        public string Description { get; set; }

        [DocumentProperty]
        public DemoColor Color { get; set; }

        [DocumentProperty]
        public DateTime Date { get; set; }

    }
}