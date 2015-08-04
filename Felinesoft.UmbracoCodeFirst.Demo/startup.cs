using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Demo.DocTypes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System.Diagnostics;

namespace Felinesoft.UmbracoCodeFirst.Demo
{
    public class startup : ApplicationEventHandler
    {
        private string _filePath = System.IO.Path.Combine((AppDomain.CurrentDomain.GetData("DataDirectory") as string), "Diagnostics");

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);
            //CodeFirstManager.Current.Features.UseBuiltInPrimitiveDataTypes = false;
            //CodeFirstManager.Current.Features.AllowAllMediaTypesInDefaultFolder = false;
            CodeFirstManager.Current.Features.UseBuiltInMediaTypes = false;
            CodeFirstManager.Current.Features.EnablePerformanceDiagnosticTimer = true;
            CodeFirstManager.Current.Initialise(typeof(HeptagonCapital.BL.DocumentTypes.Pages.Homepage).Assembly);
            Diagnostics.Timing.SaveReport(_filePath);
            CodeFirstManager.Current.Features.EnablePerformanceDiagnosticTimer = false;
            //CodeFirstManager.Current.GenerateTypeFilesFromDatabase("E:\\types", "Felinesoft.UmbracoCodeFirst.Demo");
            //CodeFirstManager.Current.Features.HideCodeFirstEntitiesInTrees = true;

            //var cs = Umbraco.Core.ApplicationContext.Current.Services.ContentService;
            //var node = cs.GetChildren(cs.GetRootContent().First().Id).First(x => x.Name.Contains("Test"));
            //node.ConvertToModel<TestPage>();
        }
    }
}