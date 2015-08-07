using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System.Diagnostics;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Demo.NewDocTypes;

namespace Felinesoft.UmbracoCodeFirst.Demo
{
    public class startup : ApplicationEventHandler
    {
        private string _filePath = System.IO.Path.Combine((AppDomain.CurrentDomain.GetData("DataDirectory") as string), "Diagnostics");

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);
            //GENERATE TYPES PRE-INIT
            //extension method AddDefaultModules in namespace Felinesoft.UmbracoCodeFirst.Extensions
            //CodeFirstManager.Current.Modules.AddDefaultModules();
            //CodeFirstManager.Current.Modules.FreezeResolver();
            //CodeFirstManager.Current.GenerateTypeFilesFromDatabase("E:\\types", "Felinesoft.UmbracoCodeFirst.Demo");

            //CONFIGURE FEATURES
            //CodeFirstManager.Current.Features.UseBuiltInPrimitiveDataTypes = false;
            //CodeFirstManager.Current.Features.AllowAllMediaTypesInDefaultFolder = false;
            //CodeFirstManager.Current.Features.UseLazyLoadingProxies = false;
            //CodeFirstManager.Current.Features.AllowReparenting = true;
            //CodeFirstManager.Current.Features.HideCodeFirstEntitiesInTrees = true;
            CodeFirstManager.Current.Features.UseContextualAttributes = true;
            CodeFirstManager.Current.Features.UseBuiltInMediaTypes = false;

            //INIT
            CodeFirstManager.Current.Features.EnablePerformanceDiagnosticTimer = true;
            CodeFirstManager.Current.Initialise(this.GetType().Assembly);
            Diagnostics.Timing.SaveReport(_filePath);
            CodeFirstManager.Current.Features.EnablePerformanceDiagnosticTimer = false;
            //CodeFirstManager.Current.GenerateTypeFilesFromDatabase("E:\\types", "Felinesoft.UmbracoCodeFirst.Demo");
        }
    }
}