using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MemberPropertyAttribute : ContentPropertyAttribute
    {
        public MemberPropertyAttribute(string name = null, string alias = null, bool memberCanEdit = false, bool showOnProfile = false, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null)
            : base(name, alias, mandatory, description, sortOrder, addTabAliasToPropertyAlias, dataType)
        {
            ShowOnProfile = showOnProfile;
            MemberCanEdit = memberCanEdit;
        }

        public bool MemberCanEdit { get; set; }

        public bool ShowOnProfile { get; set; }
    }
}