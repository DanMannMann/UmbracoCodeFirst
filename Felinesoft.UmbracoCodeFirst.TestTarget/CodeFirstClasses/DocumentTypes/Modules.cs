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

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"Modules", @"Modules", new Type[] { typeof(Moduleclassesgrid), typeof(Moduleinstructor), typeof(Modulejointhetribe), typeof(Moduleclubandfacilities), typeof(Moduleshop), typeof(Moduleclasseshero), typeof(Moduleclassinfo), typeof(Moduleclasslearnthemoves), typeof(Moduletestimonial), typeof(Modulemusictracklist), typeof(Modulegear), typeof(Modulerelatedclasses), typeof(Moduledomore), typeof(Moduleclasseslisting), typeof(Modulemusicarchivecta), typeof(Modulehero), typeof(Modulesolutions), typeof(Modulebusinesscalculators), typeof(Moduleform), typeof(Moduleinstructorsheading), typeof(Modulebenefits), typeof(Modulenextsteps), typeof(Submodulenextstepsitem), typeof(Modulesecondary), typeof(Modulelist), typeof(Modulesupporting), typeof(MODULEFAQ), typeof(Modulemediagallery), typeof(Modulesearch), typeof(Modulemorenews), typeof(Modulearticle), typeof(Modulehtmlsitemap), typeof(Modulenewform), typeof(Moduleondemandhero), typeof(Moduleondemandimages), typeof(Moduleondemandticks), typeof(Moduleherov2), typeof(Modulebanner) }, @"icon-indent", false, false, @"")]
    public class Modules : Master
    {
        public class ContentTab : TabBase
        {
        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentProperty(@"Hide in Navigation", "umbracoNaviHide", false, @"", 0, false)]
        public Checkbox Umbraconavihide { get; set; }
    }
}