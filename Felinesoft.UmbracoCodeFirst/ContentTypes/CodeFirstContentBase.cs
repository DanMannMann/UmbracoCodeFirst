
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    public abstract class CodeFirstContentBase 
    {

    }

    public abstract class CodeFirstContentBase<T> : CodeFirstContentBase where T : ContentNodeDetails
    {
        /// <summary>
        /// Gets the details of the represented node
        /// </summary>
        public virtual T NodeDetails { get; internal set; }

        public override string ToString()
        {
            return NodeDetails == null || NodeDetails.Url == null ? string.Empty : NodeDetails.Url;
        }

    }
}