using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
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

    public class PreValueFileAttribute : PreValueFactoryAttribute
    {
        private string _relativePath;

        public PreValueFileAttribute(string relativePath)
            : base(typeof(FilePreValueFactory))
        {
            _relativePath = relativePath;
        }

        public override IPreValueFactory GetFactory()
        {
            return new FilePreValueFactory(_relativePath);
        }
    }
}
