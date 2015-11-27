using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace UmbracoCodeFirst.GeneratedTypes
{
    [DocumentType(@"Repository Root", @"RepositoryRoot", new Type[] { typeof(Authorfolder), typeof(Formsfolder), typeof(Musicplaylist), typeof(Contourdatafolder), typeof(Localisationfolder) }, @"icon-folders", true, false, @"")]
    public class Repositoryroot : Master
    {
    }
}