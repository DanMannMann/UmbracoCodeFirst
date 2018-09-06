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
    [DocumentType(@"Repository Root", @"RepositoryRoot", new Type[] { typeof(Authorfolder), typeof(Formsfolder), typeof(Musicplaylist), typeof(Contourdatafolder), typeof(Localisationfolder) }, @"icon-folders", true, false, @"")]
    public class Repositoryroot : Master
    {
    }
}