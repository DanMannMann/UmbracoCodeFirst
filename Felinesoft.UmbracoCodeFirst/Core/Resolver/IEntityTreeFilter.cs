using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace Felinesoft.UmbracoCodeFirst.Core.Resolver
{
    public interface IEntityTreeFilter
    {
        bool IsFilter(string treeAlias);

        void Filter(TreeNodeCollection nodes, out bool changesMade);
    }
}
