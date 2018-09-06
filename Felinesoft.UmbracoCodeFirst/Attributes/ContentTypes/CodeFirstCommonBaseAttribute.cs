using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Marsman.UmbracoCodeFirst.Extensions;
using Marsman.UmbracoCodeFirst.Exceptions;

namespace Marsman.UmbracoCodeFirst.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CodeFirstCommonBaseAttribute : CodeFirstAttribute
    {

    }

}