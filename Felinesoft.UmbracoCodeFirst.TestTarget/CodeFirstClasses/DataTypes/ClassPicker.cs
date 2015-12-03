
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

namespace LMI.BusinessLogic.CodeFirst
{
    [DataType("Umbraco.MultiNodeTreePicker", "Class Picker")]
    [PreValue("startNode", @"{
  ""type"": ""content""
}")]
    [PreValue("filter", @"Class,SubModuleProxyClass")]
    [PreValue("minNumber", @"")]
    [PreValue("maxNumber", @"")]
    public class ClassPicker : IUmbracoNvarcharDataType
    {
        //TODO implement the properties and serialisation logic for the Umbraco.MultiNodeTreePicker property editor's values

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