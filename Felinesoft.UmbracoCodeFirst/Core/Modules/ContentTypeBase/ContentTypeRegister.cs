using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class ContentTypeRegister
    {
        private ContentTypeRegisterController _controller;
        private ConcurrentDictionary<string, ContentTypeRegistration> _registerByAlias = new ConcurrentDictionary<string, ContentTypeRegistration>();
        private ConcurrentDictionary<Type, ContentTypeRegistration> _registerByType = new ConcurrentDictionary<Type, ContentTypeRegistration>();

        private Type StripProxy(Type type)
        {
            //Remove the proxy wrapper if needed
            if (CodeFirstManager.Current.Features.UseLazyLoadingProxies && type.FullName.StartsWith("Castle.Proxies"))
            {
                return type.BaseType;
            }
            else
            {
                return type;
            }
        }

        public ContentTypeRegister(out ContentTypeRegisterController controller)
        {
            controller = new ContentTypeRegisterController(this);
        }

        public bool TryGetContentType(string alias, out ContentTypeRegistration registration)
        {
            return _registerByAlias.TryGetValue(alias, out registration);
        }

        public bool TryGetContentType(Type type, out ContentTypeRegistration registration)
        {
            type = StripProxy(type);
            return _registerByType.TryGetValue(type, out registration);
        }

        public ContentTypeRegistration GetContentType(Type type)
        {
            type = StripProxy(type);
            return _registerByType[type];
        }

        public ContentTypeRegistration GetContentType(string alias)
        {
            return _registerByAlias[alias];
        }

        public bool IsRegistered(Type type)
        {
            type = StripProxy(type);
            return _registerByType.ContainsKey(type);
        }

        public bool IsRegistered(string alias)
        {
            return _registerByAlias.ContainsKey(alias);
        }

        public IReadOnlyList<ContentTypeRegistration> Registrations
        {
            get
            {
                return _registerByType.Values.ToList().AsReadOnly();
            }
        }

        public class ContentTypeRegisterController
        {
            private ContentTypeRegister _instance;

            internal ContentTypeRegisterController(ContentTypeRegister instance)
            {
                _instance = instance;
            }

            public void Register(ContentTypeRegistration registration)
            {
                if (!_instance._registerByAlias.TryAdd(registration.Alias, registration))
                {
                    throw new CodeFirstException("Document type alias already registered");
                }

                if (!_instance._registerByType.TryAdd(registration.ClrType, registration))
                {
                    ContentTypeRegistration r;
                    _instance._registerByAlias.TryRemove(registration.Alias, out r);
                    throw new CodeFirstException("Document type CLR type already registered");
                }
            }
        }
    }
}
