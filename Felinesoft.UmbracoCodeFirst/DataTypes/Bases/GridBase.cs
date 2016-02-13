using Felinesoft.UmbracoCodeFirst.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Web.Mvc.Html;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    [DataType(propertyEditorAlias:"Umbraco.Grid")]
    [DoNotSyncDataType][BuiltInDataType]
    public abstract class GridBase : IUmbracoNtextDataType, IHtmlString
    {
        private dynamic _json = new ExpandoObject();
        private string _framework;
        private HtmlHelper _helper;

        public HtmlHelper Helper
        {
            get { return _helper; }
            set { _helper = value; }
        }

        public GridBase(string framework = "bootstrap3")
        {
            _framework = framework;
        }

        public string Framework
        {
            get
            {
                return _framework;
            }
        }

        public MvcHtmlString GridHtml
        {
            get
            {
                var view = "Grid/" + _framework;
                HtmlHelper html = _helper == null ? CreateHtmlHelper(_json) : _helper;
                return html.Partial(view, (object)_json);
            }
        }

        public dynamic Json
        {
            get
            {
                return _json;
            }
            set
            {
                _json = value;
            }
        }

        public override string ToString()
        {
            return GridHtml.ToString();
        }

        public string ToHtmlString()
        {
            return GridHtml.ToHtmlString();
        }

        //Robbed from Umbraco Core - marked obsolete there but serves a purpose here
        private static HtmlHelper CreateHtmlHelper(object model)
        {
            var cc = new ControllerContext
            {
                RequestContext = UmbracoContext.Current.HttpContext.Request.RequestContext
            };
            var viewContext = new ViewContext(cc, new FakeView(), new ViewDataDictionary(model), new TempDataDictionary(), new StringWriter());
            var htmlHelper = new HtmlHelper(viewContext, new ViewPage());
            return htmlHelper;
        }

        //Robbed from Umbraco Core - marked obsolete there but serves a purpose here
        private class FakeView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            {
            }
        }

        public void Initialise(string dbValue)
        {
            _json = JObject.Parse(dbValue);
        }

        public string Serialise()
        {
            return JsonConvert.SerializeObject(_json);
        }
    }
}
