using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public interface IPreValueFactory
    {
        IDictionary<string, PreValue> GetPreValues();
    }
}
