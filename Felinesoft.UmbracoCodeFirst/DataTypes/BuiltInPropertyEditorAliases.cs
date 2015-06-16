using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    /// <summary>
    /// The aliases of Umbraco's out-of-the-box property editors
    /// </summary>
    public static class BuiltInPropertyEditorAliases
    {
#pragma warning disable 1591
        public const string Boolean = "Umbraco.TrueFalse";
        public const string Integer = "Umbraco.Integer";
        public const string TinyMce = "Umbraco.TinyMCEv3";
        public const string Textbox = "Umbraco.Textbox";
        public const string TextboxMultiple = "Umbraco.TextboxMultiple";
        public const string UploadField = "Umbraco.UploadField";
        public const string NoEdit = "Umbraco.NoEdit";
        public const string DateTime = "Umbraco.DateTime";
        public const string ColorPicker = "Umbraco.ColorPickerAlias";
        public const string FolderBrowser = "Umbraco.FolderBrowser";
        public const string DropDownMultiple = "Umbraco.DropDownMultiple";
        public const string RadioButtonList = "Umbraco.RadioButtonList";
        public const string Date = "Umbraco.Date";
        public const string DropDown = "Umbraco.DropDown";
        public const string CheckBoxList = "Umbraco.CheckBoxList";
        public const string ContentPickerAlias = "Umbraco.ContentPickerAlias";
        public const string MediaPicker = "Umbraco.MediaPicker";
        public const string MemberPicker = "Umbraco.MemberPicker";
        public const string RelatedLinks = "Umbraco.RelatedLinks";
        public const string Tags = "Umbraco.Tags";
        public const string MultipleMediaPicker = "Umbraco.MultipleMediaPicker";
        public const string Grid = "Umbraco.Grid";
        public const string MultiNodeTreePicker = "Umbraco.MultiNodeTreePicker";
#pragma warning restore 1591
    }
}
