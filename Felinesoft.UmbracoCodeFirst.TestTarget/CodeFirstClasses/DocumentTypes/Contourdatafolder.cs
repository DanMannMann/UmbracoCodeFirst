using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"Contour Data Folder", @"ContourDataFolder", new Type[] { typeof(Contourdatafolder), typeof(Countrycontact) }, @"icon-umb-contour", false, false, @"")]
    public class Contourdatafolder : Repositoryroot
    {
    }
}