
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using Umbraco.Web;
using Newtonsoft.Json;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.RelatedLinks", "Related Links")]
    [DoNotSyncDataType][BuiltInDataType]
    public class RelatedLinks : UmbracoJsonCollectionDataType<RelatedLink>
    {
        public override void Initialise(string dbValue)
        {
            base.Initialise(dbValue);
            foreach (var item in Items)
            {
                item.SetParent(this);
            }
        }
    }



}