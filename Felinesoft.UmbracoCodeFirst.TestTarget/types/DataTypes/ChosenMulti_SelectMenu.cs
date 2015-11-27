
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
    [DataType("zwebendesign.ChosenSelectMenu", "Chosen Multi-Select Menu")]
    [PreValue("1", @"[
  {
    ""value"": ""[optgroup]:Animals""
  },
  {
    ""value"": ""Cat""
  },
  {
    ""value"": ""Dog""
  },
  {
    ""value"": ""Fish""
  },
  {
    ""value"": ""[optgroup]:Shapes""
  },
  {
    ""value"": ""Circle""
  },
  {
    ""value"": ""Square""
  },
  {
    ""value"": ""Triangle""
  },
  {
    ""value"": ""Hexagon""
  },
  {
    ""value"": ""[optgroup]:Colors""
  },
  {
    ""value"": ""Red""
  },
  {
    ""value"": ""Orange""
  },
  {
    ""value"": ""Yellow""
  },
  {
    ""value"": ""Green""
  },
  {
    ""value"": ""Blue""
  },
  {
    ""value"": ""Indigo""
  },
  {
    ""value"": ""Violet""
  }
]")]
    public class ChosenMulti_SelectMenu : IUmbracoNvarcharDataType
    {
        //TODO implement the properties and serialisation logic for the zwebendesign.ChosenSelectMenu property editor's values

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