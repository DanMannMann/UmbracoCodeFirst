using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet1
{
    [DocumentType(allowAtRoot: true)]
    public class Master : DocumentTypeBase
    {
        [ContentProperty]
        public virtual Textstring MasterTextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse MasterTrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric MasterNumericRoot { get; set; }

        [ContentTab]
        public virtual MasterTab MasterTab { get; set; }
    }

    [CodeFirstCommonBase]
    public class CommonTabBase : TabBase
    {
        [ContentProperty]
        public virtual TrueFalse CommonTabBaseTrueFalse { get; set; }
    }

    public class MasterTab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor MasterRichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker MasterDatePickerTab { get; set; }
    }

    [DocumentType]
    public class Child1 : Master
    {
        [ContentProperty]
        public virtual Textstring Child1TextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse Child1TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Child1NumericRoot { get; set; }

        [ContentTab]
        public virtual Child1Tab Child1Tab { get; set; }
    }

    public class Child1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Child1RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Child1DatePickerTab { get; set; }
    }

    [DocumentType]
    public class Child2 : Master
    {
        [ContentProperty]
        public virtual Textstring Child2TextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse Child2TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Child2NumericRoot { get; set; }
    }

    [DocumentType]
    public class GrandChild1 : Child1
    {
        [ContentProperty]
        public virtual Textstring Grandchild1TextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse Grandchild1TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Grandchild1NumericRoot { get; set; }

        [ContentTab]
        public virtual GrandChild1Tab Grandchild1Tab { get; set; }
    }

    public class GrandChild1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Grandchild1RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Grandchild1DatePickerTab { get; set; }
    }
}