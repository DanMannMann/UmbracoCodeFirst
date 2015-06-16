using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Demo.DocumentTypes;
using Felinesoft.UmbracoCodeFirst;
using Umbraco.Web.Models;
using Felinesoft.UmbracoCodeFirst.Controllers;

namespace Demo.Controllers
{
    public class LocationConfigViewModel
    {
        public string Comments { get; set; }
        public bool Visited { get; set; }
        public int Times { get; set; }
    }

    public class LocationConfigController : CodeFirstController<LocationConfig, LocationConfigViewModel>
    {
        protected override LocationConfigViewModel GetViewModel(RenderModel render)
        {
            return new LocationConfigViewModel()
            {
                Comments = render.Content.Url,
                Visited = false,
                Times = 0
            };
        }
    }

    public class LocationConfigSurfaceController : SurfaceController
    {
        public ActionResult SubmitModel(LocationConfigViewModel model)
        {
            return View();
        }
    }
}