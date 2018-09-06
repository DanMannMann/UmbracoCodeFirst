using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marsman.UmbracoCodeFirst.TestTarget.TypeSet3
{
    [DocumentType(alias: "master", allowAtRoot: true, iconColor: UmbracoIconColor.Blue, allowedChildren: new Type[] { typeof(Child1), typeof(Child2), typeof(GrandChild1) })]
    [Template(isDefault: true)]
    public class MasterRenamed : DocumentTypeBase
    {
        [ContentProperty(alias: "masterTextstringRoot")]
        public virtual Textstring MasterTextstringRenamedRoot { get; set; }

        [ContentProperty]
        public virtual Checkbox MasterTrueFalseRoot { get; set; }

        [ContentTab(sortOrder: 3)]
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
        [ContentProperty(sortOrder:3)]
        public virtual RichtextEditor MasterRichtextEditorTab { get; set; }

        [ContentProperty(mandatory: true, sortOrder: 2)]
        public virtual DatePicker MasterDatePickerTab { get; set; }

        [ContentProperty(sortOrder: 1)]
        public virtual Textstring MasterTextstringTab { get; set; }
    }

    [DocumentType(icon: BuiltInIcons.IconActivity, iconColor: UmbracoIconColor.Green, description: "The Desc")]
    [Template(isDefault: true)]
    public class Child1 : MasterRenamed
    {
        [ContentProperty(sortOrder: 2)]
        public virtual Textstring Child1TextstringRoot { get; set; }

        [ContentProperty(sortOrder: 3)]
        public virtual Checkbox Child1TrueFalseRoot { get; set; }

        [ContentProperty(sortOrder: 1)]
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
    [Template(isDefault:true)]
    public class Child2 : MasterRenamed
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
        [ContentProperty(sortOrder: 4)]
        public virtual Numeric CompositionNumericRoot { get; set; }

        [ContentTab]
        public CompositionTab CompositionTab { get; set; }
    }

    public class CompositionTab : TabBase
    {
        [ContentProperty(addTabAliasToPropertyAlias: false)]
        public virtual MediaPicker<MediaChild1> CompositionTabPicker { get; set; }

        [ContentProperty]
        public virtual MultipleTextstring CompositionMultitext { get; set; }
    }

    public class Child2Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Child2RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Child2DatePickerTab { get; set; }

        [ContentProperty]
        public virtual SingleMediaPicker<MediaFolder> FolderPicker { get; set; }
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

        [ContentComposition]
        public virtual Child2Composition Composition { get; set; }
    }

    public class GrandChild1Tab : CommonTabBase
    {
        [ContentProperty]
        public virtual RichtextEditor Grandchild1RichtextEditorTab { get; set; }

        [ContentProperty]
        public virtual DatePicker Grandchild1DatePickerTab { get; set; }
    }
}