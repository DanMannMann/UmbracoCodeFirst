
using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Marsman.UmbracoCodeFirst.Core;
using Umbraco.Web;
using Newtonsoft.Json;
using System.Web;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.RelatedLinks", "Related Links")]
    [DoNotSyncDataType][BuiltInDataType]
    public class RelatedLinks : UmbracoJsonCollectionDataType<RelatedLink>
    {
		public static implicit operator RelatedLinks(RelatedLink[] values)
		{
			return new RelatedLinks() { Items = new List<RelatedLink>(values) };
		}
		
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