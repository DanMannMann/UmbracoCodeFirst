
using Marsman.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PreValueFactoryAttribute : CodeFirstAttribute
    {
        public Type FactoryType { get; private set; }

        public virtual IPreValueFactory GetFactory()
        {
            return (IPreValueFactory)Activator.CreateInstance(FactoryType);
        }

        public PreValueFactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }
    }

}
