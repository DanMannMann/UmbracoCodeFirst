using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InstanceThumbnailSizesAttribute : InstancePreValueFactoryAttribute
    {
        private int[] _sizes;

        public override IPreValueFactory GetFactory()
        {
            return new ThumbnailSizesPreValueFactory(_sizes);
        }

        public InstanceThumbnailSizesAttribute(params int[] sizes)
            : base(typeof(ThumbnailSizesPreValueFactory))
        {
            _sizes = sizes;
        }
    }
}