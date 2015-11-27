
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

namespace UmbracoCodeFirst.GeneratedTypes
{
    [DataType("BJW.Modul8", "Module 1")]
    [PreValue("1", @"[
  {
    ""value"": ""Module Header"",
    ""type"": ""TextBox""
  },
  {
    ""value"": ""Module SubHeader"",
    ""type"": ""TextBox""
  },
  {
    ""value"": ""Visible"",
    ""type"": ""Bool""
  },
  {
    ""value"": ""Max Posts"",
    ""type"": ""Number""
  },
  {
    ""value"": ""Post Type"",
    ""type"": ""Dropdown"",
    ""options"": [
      {
        ""value"": ""Tinky winky""
      },
      {
        ""value"": ""Lah Lah""
      },
      {
        ""value"": ""Dipsy""
      },
      {
        ""value"": ""Poooooooh""
      }
    ]
  }
]")]
    public class Module1 : IUmbracoNvarcharDataType
    {
        //TODO implement the properties and serialisation logic for the BJW.Modul8 property editor's values

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            throw new NotImplementedException();
        }
    }
}