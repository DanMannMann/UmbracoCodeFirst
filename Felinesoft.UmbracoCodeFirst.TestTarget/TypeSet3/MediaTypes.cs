using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet3
{
    [MediaType(allowAtRoot: true)]
    public class MediaMaster : MediaTypeBase
    {
        [ContentProperty]
        public virtual Textstring MediaMasterTextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse MediaMasterTrueFalseRoot { get; set; }

        [ContentTab]
        public virtual MasterTab MasterTab { get; set; }
    }

    public class MediaMasterTab : TabBase
    {
        [ContentProperty]
        public virtual RichtextEditor MasterRichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker MasterDatePickerTab { get; set; }

        [ContentProperty(mandatory: true)]
        public virtual Textstring MasterTextstringTab { get; set; }
    }

    [MediaType(icon: BuiltInIcons.IconActivity, iconColor: UmbracoIconColor.Green)]
    public class MediaChild1 : MediaMaster
    {
        [ContentProperty(sortOrder: 2)]
        public virtual Textstring Child1TextstringRoot { get; set; }

        [ContentProperty(sortOrder: 3)]
        public virtual TrueFalse Child1TrueFalseRoot { get; set; }

        [ContentProperty(sortOrder: 1)]
        public virtual Numeric Child1NumericRoot { get; set; }

        [ContentProperty(description: "  This be a  lABel ", mandatory: true)]
        public virtual Label Child1LabelRoot { get; set; }

        [ContentTab]
        public virtual Child1Tab Child1Tab { get; set; }
    }

    [MediaType]
    public class MediaComposition : MediaTypeBase
    {
        [ContentProperty]
        public virtual Label MediaCompositionLabel { get; set; }
    }

    public class MediaChild1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Child1RichtextEditorTab { get; set; }
    }

    [MediaType]
    public class MediaChild2 : MediaMaster
    {
        [ContentProperty]
        public virtual Textstring Child2TextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse Child2TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Child2NumericRoot { get; set; }

        [ContentTab]
        public virtual Child2Tab Child2Tab { get; set; }

        [ContentComposition]
        public MediaComposition Composition { get; set; }

        [ContentComposition]
        public virtual MediaChild2Composition Composition2 { get; set; }
    }

    [MediaType]
    public class MediaChild2Composition : MediaTypeBase
    {
        [ContentProperty]
        public virtual Numeric CompositionNumericRoot { get; set; }

        //TODO add a tab to this in TypeSet3
    }

    [MediaType]
    public class MediaGrandChild1 : MediaChild1
    {
        [ContentProperty]
        public virtual Textstring Grandchild1TextstringRoot { get; set; }

        [ContentProperty]
        public virtual TrueFalse Grandchild1TrueFalseRoot { get; set; }

        [ContentProperty]
        public virtual Numeric Grandchild1NumericRoot { get; set; }
    }

    public class MediaGrandChild1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Grandchild1RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Grandchild1DatePickerTab { get; set; }
    }
}