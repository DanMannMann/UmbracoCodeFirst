using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet2
{
    [DocumentType(allowAtRoot: true)]
    [Template(isDefault: true)]
    public class Master : DocumentTypeBase
    {
        [ContentProperty]
        public virtual Textstring MasterTextstringRoot { get; set; }

        [ContentProperty]
        public virtual Checkbox MasterTrueFalseRoot { get; set; }

        [ContentTab]
        public virtual MasterTab MasterTab { get; set; }
    }

    [CodeFirstCommonBase]
    public class CommonTabBase : TabBase
    {
        [ContentProperty]
        public virtual Checkbox CommonTabBaseTrueFalse { get; set; }
    }

    public class MasterTab : TabBase
    {
        [ContentProperty]
        public virtual RichtextEditor MasterRichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker MasterDatePickerTab { get; set; }

        [ContentProperty(mandatory: true)]
        public virtual Textstring MasterTextstringTab { get; set; }
    }

    [DocumentType(icon: BuiltInIcons.IconActivity, iconColor: UmbracoIconColor.Green)]
    [Template(isDefault: true)]
    public class Child1 : Master
    {
        [ContentProperty]
        public virtual Textstring Child1TextstringRoot { get; set; }

        [ContentProperty]
        public virtual Checkbox Child1TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Child1NumericRoot { get; set; }

        [ContentProperty(description: "  This be a  lABel ", mandatory: true)]
        public virtual Label Child1LabelRoot { get; set; }

        [ContentTab]
        public virtual Child1Tab Child1Tab { get; set; }
    }

    public class Child1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Child1RichtextEditorTab { get; set; }
    }

    [DocumentType]
    public class Child2 : Master
    {
        [ContentProperty]
        public virtual Textstring Child2TextstringRoot { get; set; }

        [ContentProperty]
        public virtual Checkbox Child2TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Child2NumericRoot { get; set; }

        [ContentTab]
        public virtual Child2Tab Child2Tab { get; set; }

        [ContentComposition]
        public virtual Child2Composition Composition { get; set; }
    }

    [DocumentType]
    public class Child2Composition : DocumentTypeBase
    {
        [ContentProperty]
        public virtual Numeric CompositionNumericRoot { get; set; }

        //TODO add a tab to this in TypeSet3
    }

    public class Child2Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Child2RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Child2DatePickerTab { get; set; }
    }

    [DocumentType]
    public class GrandChild1 : Child1
    {
        [ContentProperty]
        public virtual Textstring Grandchild1TextstringRoot { get; set; }

        [ContentProperty]
        public virtual Checkbox Grandchild1TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Grandchild1NumericRoot { get; set; }
    }

    public class GrandChild1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Grandchild1RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Grandchild1DatePickerTab { get; set; }
    }
}