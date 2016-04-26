using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CodeFirstCommonBaseAttribute : CodeFirstAttribute
    {

    }

}