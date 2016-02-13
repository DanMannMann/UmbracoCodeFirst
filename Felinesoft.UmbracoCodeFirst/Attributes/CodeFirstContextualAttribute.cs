
using System;
using System.Collections;
using System.Collections.Generic;
namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    public abstract class CodeFirstContextualAttribute : MultipleCodeFirstAttribute
    {
        public abstract string CombineToOutputString(IEnumerable<CodeFirstContextualAttribute> input);
    }

    public abstract class HtmlTagContextualAttribute : CodeFirstContextualAttribute
    {
        
    }
}