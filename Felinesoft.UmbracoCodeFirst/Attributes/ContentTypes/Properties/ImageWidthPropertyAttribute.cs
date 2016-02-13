using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ImageWidthPropertyAttribute : ContentPropertyAttribute
    {
        public ImageWidthPropertyAttribute(string name = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null)
            : base(name, SpecialAliases.ImageWidth, mandatory, description, sortOrder, addTabAliasToPropertyAlias, dataType)
        {

        }
    }
}