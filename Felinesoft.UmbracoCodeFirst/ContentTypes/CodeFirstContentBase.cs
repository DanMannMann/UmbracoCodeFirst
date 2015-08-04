
using System.Web;
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
            return NodeDetails.Url;
        }
    }
}