using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Linq;
using System.Reflection;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class NodePickerConfigAttribute : DataTypeInstanceAttribute,IInitialisableAttribute, IInitialisablePropertyAttribute
    {
        private int _minimumItems;
        private int _maximumItems;
        private string _startNodeInput;
        private StartNodeSpecifier _startNodeMode;
        private int _startNodeId;

        private bool _initialised;

        public NodePickerConfigAttribute(int minimumItems = 0, int maximumItems = 0, string startNode = null, StartNodeSpecifier startNodeSpecifier = StartNodeSpecifier.Path)
        {
            _minimumItems = minimumItems;
            _maximumItems = maximumItems;
            _startNodeMode = startNodeSpecifier;
            _startNodeInput = startNode;
        }

        public int MinimumItems
        {
            get { return _minimumItems; }
        }

        public int MaximumItems
        {
            get { return _maximumItems; }
        }

        public int StartNodeId
        {
            get { return _startNodeId; }
        }

        public bool Initialised
        {
            get { return _initialised; }
        }

        public void Initialise(PropertyInfo propertyTarget)
        {
            Init(propertyTarget.DeclaringType.Name + "." + propertyTarget.Name);
        }

        public void Initialise(Type decoratedType)
        {
            Init(decoratedType.Name);
        }

        private void Init(string memberName)
        {
            switch (_startNodeMode)
            {
                case StartNodeSpecifier.Path:
                    try
                    {
                        _startNodeId = GetNodeIdFromPath(_startNodeInput);
                    }
                    catch (Exception ex)
                    {
                        throw new CodeFirstException("Unexpected error when initialising [NodePickerConfigAttribute] with StartNodeSpecifier.Path and input string " + (string.IsNullOrWhiteSpace(_startNodeInput) ? "[null or empty]" : _startNodeInput) + ". Affected member: " + memberName, ex);
                    }
                    break;
                case StartNodeSpecifier.Id:
                    try
                    {
                        _startNodeId = int.Parse(_startNodeInput);
                    }
                    catch (Exception ex)
                    {
                        throw new CodeFirstException("startNode must be a string representation of a valid integer if StartNodeSpecifier.Id is specified. Affected member: " + memberName, ex);
                    }
                    break;
                default:
                    throw new CodeFirstException("StartNodeSpecifier type " + _startNodeMode.ToString() + " is not supported. Affected member: " + memberName);
            }
            _initialised = true;
        }

        private int GetNodeIdFromPath(string startNode)
        {
            if (startNode == null || string.IsNullOrWhiteSpace(startNode))
            {
                return -1;
            }
            var helper = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current);
            var rootMedia = helper.TypedMediaAtRoot();
            var pieces = _startNodeInput.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            if (pieces.Length == 0)
            {
                return -1;
            }
            IPublishedContent current = rootMedia.FirstOrDefault(x => x.Name == pieces[i]);
            i++;
            while (current != null && i < pieces.Length)
            {
                current = current.Children.FirstOrDefault(x => x.Name == pieces[i]);
            }
            if (current == null)
            {
                return -1;
            }
            else
            {
                return current.Id;
            }
        }
    }
}