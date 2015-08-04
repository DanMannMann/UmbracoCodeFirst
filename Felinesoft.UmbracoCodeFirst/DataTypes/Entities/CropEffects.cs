using System;
using System.Drawing;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public class CropEffects
    {
        private ImageCropDataSet _underlying;
        private string _alias;
        private bool _isOriginal;

        public void SetUnderlyingDataset(ImageCropDataSet underlying, string alias = null)
        {
            _underlying = underlying;
            _isOriginal = alias == null;
            _alias = alias;
        }

        public MvcHtmlString RoundedCorners(int radius, Color backgroundColor)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?roundedcorners=" + radius.ToString() + "&bgcolor=" + GetHexColor(backgroundColor));
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&roundedcorners=" + radius.ToString() + "&bgcolor=" + GetHexColor(backgroundColor));
            }
        }

        public MvcHtmlString RoundedCorners(int radius)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?roundedcorners=" + radius.ToString());
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&roundedcorners=" + radius.ToString());
            }
        }

        public MvcHtmlString Rotate(int degrees, Color backgroundColor)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?rotate=" + degrees.ToString() + "&bgcolor=" + GetHexColor(backgroundColor));
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&rotate=" + degrees.ToString() + "&bgcolor=" + GetHexColor(backgroundColor));
            }
        }

        public MvcHtmlString Rotate(int degrees)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?rotate=" + degrees.ToString());
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&rotate=" + degrees.ToString());
            }
        }

        public MvcHtmlString Flip(FlipType type)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?flip=" + type.ToString().ToLower());
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&flip=" + type.ToString().ToLower());
            }
        }

        public MvcHtmlString Filter(FilterType type)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?filter=" + type.ToString().ToLower());
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&filter=" + type.ToString().ToLower());
            }
        }

        public MvcHtmlString Blur(int radius, double sigma, int threshold)
        {
            if (_isOriginal)
            {
                return new MvcHtmlString(_underlying.Src + "?blur=" + radius.ToString() + "&sigma=" + string.Format("{0:0.##}", sigma) + "&threshold=" + threshold.ToString());
            }
            else
            {
                return new MvcHtmlString(_underlying.Src + _underlying.GetCropUrl(_alias, useFocalPoint: _underlying.HasFocalPoint()) + "&blur=" + radius.ToString() + "&sigma=" + string.Format("{0:0.##}", sigma) + "&threshold=" + threshold.ToString());
            }
        }

        private string GetHexColor(Color backgroundColor)
        {
            return String.Format("{0:X2}{1:X2}{2:X2}", backgroundColor.R, backgroundColor.G, backgroundColor.B);
        }
    }
}