using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;

namespace Felinesoft.UmbracoCodeFirst.Demo.NewDocTypes
{
    [DocumentType]
    public class Master : DocumentTypeBase
    {
        [ContentProperty]
        public virtual TrueFalse MasterBooleanRoot { get; set; }

        [ContentProperty]
        public virtual Textstring MasterStringRoot { get; set; }

        [ContentTab]
        public MasterTab MasterTab { get; set; }
    }

    [DocumentType]
    public class JamMasterJ : DocumentTypeBase
    {
        [ContentProperty]
        public virtual TrueFalse JamMasterJBooleanRoot { get; set; }

        [ContentProperty]
        public virtual Textstring JamMasterJStringRoot { get; set; }

        [ContentTab]
        public JamMasterJTab MasterTab { get; set; }
    }

    [DocumentType]
    public class Child : Master
    {
        [ContentProperty]
        public virtual Label ChildLabelRoot { get; set; }

        [ContentProperty]
        public virtual Tags ChildTagsRoot { get; set; }

        [ContentTab]
        public ChildTab ChildTab { get; set; }
    }

    [DocumentType]
    public class Grandchild : Child, IOnCreate
    {
        [ContentProperty]
        public virtual TextboxMultiple GrandchildTextMultipleRoot { get; set; }

        [ContentProperty]
        public virtual Label GrandchildLabelRoot { get; set; }

        [ContentTab]
        public new GrandchildTab ChildTab { get; set; }

        public void OnCreate()
        {
            GrandchildLabelRoot = new Label() { Value = @"Here's text" };
            NodeDetails.Name = Guid.NewGuid().ToString();
        }
    }

    public class JamMasterJTab : TabBase
    {
        [ContentProperty]
        public virtual Label MasterRichtextTab { get; set; }

        [ContentProperty]
        public virtual Textstring MasterNumericTab { get; set; }
    }

    public class MasterTab : TabBase
    {
        [ContentProperty]
        public virtual RichtextEditor MasterRichtextTab { get; set; }

        [ContentProperty]
        public virtual Numeric MasterNumericTab { get; set; }
    }

    public class ChildTab : TabBase
    {
        [ContentProperty]
        public virtual Label ChildLabelTab { get; set; }

        [ContentProperty]
        public virtual ColorPicker ChildColorPickerTab { get; set; }
    }

    public class GrandchildTab : ChildTab
    {
        [ContentProperty]
        public virtual Numeric GrandchildNumericTab { get; set; }
    }
}