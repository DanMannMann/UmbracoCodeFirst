using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;

namespace Demo.DataTypes
{
    [DataType("gpsEditor")]
    public class GpsCoordinates : UmbracoJsonDataType
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return Latitude.ToString() + "," + Longitude.ToString();
        }
    }
}