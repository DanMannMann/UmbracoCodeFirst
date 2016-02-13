using System;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.Core
{
    public sealed class CodeFirstLazyInitialiser
    {
        private Action _initialiser;
        private bool _invoked;
        private object _lock = new object();

        public PropertyInfo TargetProperty { get; set; }

        public CodeFirstLazyInitialiser(Action initialiser)
        {
            _initialiser = initialiser;
        }

        public void Execute()
        {
            if (!_invoked)
            {
                _initialiser.Invoke();
                _invoked = true;
            }
        }

        public void Complete()
        {
            _invoked = true;
        }

        public bool IsDone
        {
            get
            {
                return _invoked;
            }
        }
    }
}