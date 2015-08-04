using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ThumbnailSizesAttribute : PreValueFactoryAttribute
    {
        private int[] _sizes;

        public override IPreValueFactory GetFactory()
        {
            return new ThumbnailSizesPreValueFactory(_sizes);
        }

        public ThumbnailSizesAttribute(params int[] sizes)
            : base(typeof(ThumbnailSizesPreValueFactory))
        {
            _sizes = sizes;
        }
    }

}