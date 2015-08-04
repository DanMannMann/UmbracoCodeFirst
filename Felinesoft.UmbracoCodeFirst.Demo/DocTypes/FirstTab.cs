using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    public class FirstTab : TabBase
    {
        [ContentProperty]
        public virtual CheckboxList CheckboxList { get; set; }

        [ContentProperty]
        public virtual ColorPicker ColorPicker { get; set; }

        [ContentProperty]
        public virtual LegacyContentPicker ContentPicker { get; set; }

        [ContentProperty]
        public virtual DatePicker DatePicker { get; set; }

        [ContentProperty]
        public virtual DatePickerWithTime DatePickerWithTime { get; set; }

        [ContentProperty]
        public virtual DropdownList DropdownList { get; set; }

        [ContentProperty]
        public virtual DropdownMultiple DropdownMultiple { get; set; }

        [ContentProperty]
        public virtual Label Label { get; set; }

        [ContentProperty(dataType: BuiltInDataTypes.DatePicker)]
        public virtual DateTime DayteThyme { get; set; }

        [ContentProperty(dataType: BuiltInDataTypes.DatePickerWithTime)]
        public virtual DateTime DaveTime { get; set; }

        [ContentProperty]
        [DataTypeInstance(BuiltInPropertyEditorAliases.MultipleTextstring, name: "Multipass", dbType: DatabaseType.Ntext)]
        [InstancePreValue("0", "{\"Minimum\":3,\"Maximum\":3}")]
        public virtual IEnumerable<string> TextLinesAGAIN { get; set; }

        [ContentProperty]
        [DataTypeInstance(BuiltInPropertyEditorAliases.MultipleTextstring, name: "Multipass 2", dbType: DatabaseType.Ntext)]
        [InstancePreValue("0", "{\"Minimum\":2,\"Maximum\":2}")]
        public virtual IEnumerable<string> TextLinesAGAIN2 { get; set; }

        [ContentProperty(alias: "stringyAgain_Second_Tab", addTabAliasToPropertyAlias: false)]
        public virtual Textstring StringyThingyMoved { get; set; }
    }
}