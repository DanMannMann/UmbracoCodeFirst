using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public interface IPreValueFactory
    {
        IDictionary<string, PreValue> GetPreValues(PreValueContext context);
    }

    public sealed class PreValueContext
    {
        public enum PreValueContextType
        {
            None,
            Property,
            Class,
            Unknown
        }

        public PreValueContext(MemberInfo member)
        {
            CurrentMember = member;
        }

        public PreValueContextType ContextType
        {
            get
            {
                if (CurrentMember == null)
                {
                    return PreValueContextType.None;
                }
                else if (CurrentMember is PropertyInfo)
                {
                    return PreValueContextType.Property;
                }
                else if (CurrentMember is System.Type)
                {
                    return PreValueContextType.Class;
                }
                else
                {
                    return PreValueContextType.Unknown;
                }
            }
        }
        public MemberInfo CurrentMember { get; private set; }
    }
}
