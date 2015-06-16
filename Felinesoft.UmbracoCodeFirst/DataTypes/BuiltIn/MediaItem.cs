using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    public class MediaItem : XPathItem, IMediaItem
    {
        private bool _initialised;
        private bool _idSet;
        private int _id;
        private string _url;
        private string _name;
        private string _extension;
        private int _width;
        private int _height;
        private int _fileSize;
        private MediaType _type;

        public int MediaNodeId
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                _idSet = true;
            }
        }

        public string Url
        {
            get { init(); return _url; }
            protected set { _url = value; }
        }

        public string Name
        {
            get { init(); return _name; }
            protected set { _name = value; }
        }

        public string Extension
        {
            get { init(); return _extension; }
            protected set { _extension = value; }
        }

        public int Width
        {
            get { init(); return _width; }
            protected set { _width = value; }
        }

        public int Height
        {
            get { init(); return _height; }
            protected set { _height = value; }
        }

        /// <summary>
        /// The size of the file in bytes
        /// </summary>
        /// 
        public int FileSize
        {
            get { init(); return _fileSize; }
            protected set { _fileSize = value; }
        }

        public MediaType Type
        {
            get { init(); return _type; }
            protected set { _type = value; }
        }

        protected virtual void init()
        {
            if (!_initialised)
            {
                if (_idSet)
                {
                    var iterator = umbraco.library.GetMedia(MediaNodeId, false);
                    Url = GetCData(iterator, "umbracoFile");
                    Extension = GetCData(iterator, "umbracoExtension");
                    Height = int.Parse(GetCData(iterator, "umbracoHeight"));
                    Width = int.Parse(GetCData(iterator, "umbracoWidth"));
                    Name = GetAttribute(iterator, "nodeName");
                    var type = GetAttribute(iterator, "nodeTypeAlias");
                    MediaType mType;
                    if (!Enum.TryParse<MediaType>(type, out mType))
                    {
                        Type = MediaType.Custom;
                    }
                    else
                    {
                        Type = mType;
                    }
                    _initialised = true;
                }
                else
                {
                    throw new CodeFirstException("No ID is set");
                }
            }
        }

        public override string ToString()
        {
            return Url;
        }
    }

    public class XPathItem
    {
        /// <summary>
        /// Gets a CData value from an XML node as specified by an xpath
        /// </summary>
        /// <param name="iterator">The node iterator to get the node from</param>
        /// <param name="xpath">The path to the node</param>
        /// <returns>The value of the node</returns>
        protected string GetCData(XPathNodeIterator iterator, string xpath)
        {
            XPathNavigator cdata = iterator.Current.SelectSingleNode(xpath);
            if (cdata != null)
            {
                return (string)cdata.TypedValue;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Gets an attribute value from an XML node
        /// </summary>
        /// <param name="iterator">The node iterator to get the node from</param>
        /// <param name="attributeName">The attribute of the node to get</param>
        /// <returns>The value of the attribute</returns>
        protected string GetAttribute(XPathNodeIterator iterator, string attributeName)
        {
            return iterator.Current.GetAttribute(attributeName, "");
        }
    }
}
