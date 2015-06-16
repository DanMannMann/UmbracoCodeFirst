using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Controllers;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using umbraco.cms.businesslogic.datatype;
using Umbraco.Core;
using Umbraco.Web.Mvc;

namespace Demo
{
    public class Startup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            //Initialise code-first using the types in the current assembly
            Felinesoft.UmbracoCodeFirst.CodeFirstManager.Current.Initialise(this.GetType().Assembly);
        }

        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(CodeFirstController));
            base.ApplicationStarting(umbracoApplication, applicationContext);
        }
    }
}