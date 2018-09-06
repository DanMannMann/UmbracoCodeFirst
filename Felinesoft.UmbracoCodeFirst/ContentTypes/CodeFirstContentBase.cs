
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using System.Linq;

namespace Marsman.UmbracoCodeFirst.ContentTypes
{
    public abstract class CodeFirstContentBase 
    {
        public virtual ContentNodeDetails NodeDetails { get; internal set; }
    }

    public abstract class CodeFirstContentBase<T> : CodeFirstContentBase where T : ContentNodeDetails
    {
        private T _node;

        /// <summary>
        /// Gets the details of the represented node
        /// </summary>
        public virtual new T NodeDetails
        {
            get { return base.NodeDetails as T; }
            internal set { base.NodeDetails = value; }
        }
    }
}