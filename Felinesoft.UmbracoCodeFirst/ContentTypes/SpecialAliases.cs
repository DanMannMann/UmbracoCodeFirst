using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.ContentTypes
{
    public static class SpecialAliases
    {
        public const string FileUpload = Umbraco.Core.Constants.Conventions.Media.File;
        public const string NavigationHide = Umbraco.Core.Constants.Conventions.Content.NaviHide;
        public const string FileSize = Umbraco.Core.Constants.Conventions.Media.Bytes;
        public const string FileExtension = Umbraco.Core.Constants.Conventions.Media.Extension;
        public const string ImageHeight = Umbraco.Core.Constants.Conventions.Media.Height;
        public const string ImageWidth = Umbraco.Core.Constants.Conventions.Media.Width;
    }
}
