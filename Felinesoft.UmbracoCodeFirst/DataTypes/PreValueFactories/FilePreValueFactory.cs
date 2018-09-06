using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Marsman.UmbracoCodeFirst.DataTypes
{
    public class FilePreValueFactory : IPreValueFactory
    {
        private string _path;

        public FilePreValueFactory(string relativePath)
        {
            _path = System.Web.Hosting.HostingEnvironment.MapPath(relativePath);
        }

        public IDictionary<string, Umbraco.Core.Models.PreValue> GetPreValues(PreValueContext context)
        {
            var doc = new XmlDocument();
            doc.Load(_path);
            var result = new Dictionary<string, Umbraco.Core.Models.PreValue>();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var value = node.InnerText.Trim();
                var alias = node.Attributes["alias"].InnerText;
                int sortOrder = 0;
                if (node.Attributes["sortOrder"] != null)
                {
                    sortOrder = int.Parse(node.Attributes["sortOrder"].InnerText);
                }
                result.Add(alias, new Umbraco.Core.Models.PreValue(0, value, sortOrder));
            }

            return result;
        }
    }

}
