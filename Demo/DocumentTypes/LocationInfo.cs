using Demo.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Content;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;

namespace Demo.DocumentTypes
{
    [DocumentType]
    [AutoContent("Location - London")]
    public class LocationInfo : DocumentTypeBase, IAutoContent
    {
        [DocumentProperty]
        public string Name { get; set; }

        [DocumentProperty]
        public string Description { get; set; }

        [DocumentProperty]
        public DemoColor Color { get; set; }

        [DocumentProperty]
        public DateTime Date { get; set; }

        public void InitialiseDefaults()
        {
            Name = "London";
            Description = "Stinky";
            Color = new DemoColor() { Color = System.Drawing.ColorTranslator.FromHtml("#111111") };
            Date = DateTime.Parse("2013-04-02 01:43");
        }
    }
}