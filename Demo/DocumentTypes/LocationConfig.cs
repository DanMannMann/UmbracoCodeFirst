using Demo.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Demo.DocumentTypes
{
    [DocumentType(AllowedAtRoot = true)]
    public class LocationConfig : DocumentTypeBase
    {
        public class TestTab : TabBase
        {
            [DocumentProperty]
            public TypedContentPicker<LocationInfo> TypedPicker { get; set; }

            [DocumentProperty(Description = "string")]
            public string String { get; set; }

            [DocumentProperty(Description = "int")]
            public int Numeric { get; set; }

            [DocumentProperty(Description = "DateTime")]
            [Display(Name = "Visit Date and Time")]
            public DateTime DateTime { get; set; }

            [DocumentProperty(Description = "ContentPicker")]
            public ContentPicker ContentPicker { get; set; }

            [DocumentProperty(Description = "ColorPicker")]
            public ColorPicker ColorPicker { get; set; }

            [DocumentProperty(Description = "bool")]
            public bool Bool { get; set; }

            [DocumentProperty(Description = "CheckboxList")]
            public CheckboxList CheckboxList { get; set; }

            [DocumentProperty(Description = "DropdownList")]
            public DropdownList DropdownList { get; set; }

            [DocumentProperty(Description = "My Dropdown")]
            public MyDropDown DD1 { get; set; }

            [DocumentProperty(Description = "My Dropdown Instance")]
            [DataTypeInstance]
            [InstancePreValue("0", "option 0B")]
            [InstancePreValue("1", "option 1B")]
            [InstancePreValue("2", "option 2B")]
            public MyDropDown DD2 { get; set; }

            [DocumentProperty(Description = "DropdownListMultiple")]
            public DropdownMultiple DropdownListMultiple { get; set; }

            [DocumentProperty(Description = "RadioButtonList")]
            public RadioButtonList RadioButtonList { get; set; }

            [DocumentProperty(Description = "AttributeTargets enum (auto)")]
            public AttributeTargets AttributeTargets { get; set; }

            [DocumentProperty(Description = "Options enum (attr)")]
            public Options Options { get; set; }

            [DocumentProperty(Description = "Choices enum (register)")]
            public Choices Choices { get; set; }
        }

        public class Test2Tab : TabBase
        {
            [DocumentProperty(Description = "MediaPicker")]
            public MediaPicker MediaPicker { get; set; }

            [DocumentProperty(Description = "MultipleMediaPicker")]
            public MultipleMediaPicker MultipleMediaPicker { get; set; }

            [DocumentProperty(Description = "MemberPicker")]
            public MemberPicker MemberPicker { get; set; }

            [DocumentProperty(Description = "Label")]
            public Label Label { get; set; }

            [DocumentProperty(Description = "Links")]
            public RelatedLinks Links { get; set; }
        }

        [Felinesoft.UmbracoCodeFirst.Attributes.DataType]
        [PreValue("0", "option 0A")]
        [PreValue("1", "option 1A")]
        [PreValue("2", "option 2A")]
        public class MyDropDown : DropdownList { }

        public enum Choices
        {
            First,
            Second,
            Third
        }

        [Felinesoft.UmbracoCodeFirst.Attributes.EnumDataType(BuiltInPropertyEditorAliases.DropDownMultiple, "Not using the default name", typeof(EnumDataTypeConverter<Options>))]
        [Flags]
        public enum Options
        {
            None = 0,
            Fourth = 1,
            Fifth = 2,
            Sixth = 4
        }

        [DocumentTab]
        public TestTab TestProperties { get; set; }

        [DocumentTab]
        public Test2Tab Test2Properties { get; set; }

        [DocumentProperty]
        public GpsCoordinates Coordinates { get; set; }

        public void InitialiseDefaults()
        {
            Test2Properties = new Test2Tab();
            Test2Properties.Links = new RelatedLinks();
            JsonConvert.PopulateObject(vals, Test2Properties.Links);
        }

        const string vals = @"[
  {
    ""caption"": ""Starter Kit"",
    ""link"": ""/learn/the-starter-kit/"",
    ""newWindow"": true,
    ""isInternal"": true,
    ""internalName"": ""The starter kit"",
    ""title"": ""Starter Kit"",
    ""edit"": false,
    ""type"": ""internal"",
    ""internal"": 1075
  },
  {
    ""caption"": ""The Basics"",
    ""link"": ""/learn/basics/"",
    ""newWindow"": false,
    ""isInternal"": true,
    ""internalName"": ""Basics"",
    ""title"": ""The Basics"",
    ""edit"": false,
    ""type"": ""internal"",
    ""internal"": 1076
  },
  {
    ""caption"": ""Teh toobs"",
    ""link"": ""http://www.youtube.com"",
    ""newWindow"": true,
    ""edit"": false,
    ""isInternal"": false,
    ""type"": ""external"",
    ""title"": ""Teh toobs""
  }
]";
    }
}