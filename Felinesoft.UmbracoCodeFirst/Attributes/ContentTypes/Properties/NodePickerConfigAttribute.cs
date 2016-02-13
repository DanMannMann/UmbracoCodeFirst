using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Umbraco.Core.Models;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Web;
using System.Web;
using Umbraco.Web.Security;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class NodePickerConfigAttribute : CodeFirstAttribute, IDataTypeInstance, IInitialisableAttribute, IInitialisablePropertyAttribute
    {
        private int _minimumItems;
        private int _maximumItems;
        private string _startNodeInput;
        private StartNodeSpecifier _startNodeMode;
        private int _startNodeId;
        private Type _targetType;
        private Type[] _allowedDescendants;
        private NodePickerConfigAttribute _basis;
        private bool _initialised;
		private bool _showOpenButton;
		private bool _showEditButton;
		private bool _showPathsWhenHovering;

		public NodePickerConfigAttribute(int minimumItems = -1, 
										 int maximumItems = -1, 
										 Type[] allowedDescendants = null, 
										 string startNode = null, 
										 StartNodeSpecifier startNodeSpecifier = StartNodeSpecifier.Path, 
										 bool showOpenButton = false, 
										 bool showEditButton = false, 
										 bool showPathsWhenHovering = false)
        {
            _minimumItems = minimumItems;
            _maximumItems = maximumItems;
            _startNodeMode = startNodeSpecifier;
            _startNodeInput = startNode;
            _allowedDescendants = allowedDescendants;
			_showOpenButton = showOpenButton;
			_showEditButton = showEditButton;
			_showPathsWhenHovering = showPathsWhenHovering;
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

        public Type[] AllowedDescendants
        {
            get { return _allowedDescendants; }
        }

        internal string AllowedDescendantString
        {
            get
            {
                return string.Join(",", AllowedDescendants.Select(x => x.GetCodeFirstAttribute<ContentTypeAttribute>().Alias));
            }
        }

		public bool ShowOpenButton
		{
			get
			{
				return _showOpenButton;
			}
		}

		public bool ShowEditButton
		{
			get
			{
				return _showEditButton;
			}
		}

		public bool ShowPathsWhenHovering
		{
			get
			{
				return _showPathsWhenHovering;
			}
		}

		public void Initialise(PropertyInfo propertyTarget)
        {
            _targetType = propertyTarget.PropertyType;
            _basis = _targetType.GetCodeFirstAttribute<NodePickerConfigAttribute>();
            Init(propertyTarget.DeclaringType.Name + "." + propertyTarget.Name);
        }

        public void Initialise(Type decoratedType)
        {
            _targetType = decoratedType;
            if (_targetType.BaseType != null)
            {
                _basis = _targetType.BaseType.GetCodeFirstAttribute<NodePickerConfigAttribute>();
            }
            Init(decoratedType.Name);
        }

        private void Init(string memberName)
        {
            if (_allowedDescendants == null)
            {
                _allowedDescendants = new Type[] { };
            }

            if (_basis != null)
            {
                _allowedDescendants = _allowedDescendants.Union(_basis.AllowedDescendants).ToArray();
                if (_minimumItems == -1)
                {
                    _minimumItems = _basis._maximumItems;
                }
                if (_maximumItems == -1)
                {
                    _maximumItems = _basis._maximumItems;
                }
                if (_startNodeInput == null)
                {
                    _startNodeInput = _basis._startNodeInput;
                    _startNodeMode = _basis._startNodeMode;
                }
            }

            foreach (var desc in _allowedDescendants)
            {
                if (!desc.Inherits(_targetType))
                {
                    throw new CodeFirstException("Specified allowed descendant " + desc.Name + " does not inherit target type " + _targetType.Name + ". Affected node picker: " + memberName);
                }
                else if (desc.GetCodeFirstAttribute<ContentTypeAttribute>() == null)
                {
                    throw new CodeFirstException("Specified allowed descendant " + desc.Name + " does not have a [ContentType] attribute (e.g. [DocumentType], [MediaType]). Affected node picker: " + memberName);
                }
            }

            switch (_startNodeMode)
            {
                case StartNodeSpecifier.Path:
                    try
                    {
                        _startNodeId = GetNodeIdFromPath(_startNodeInput);
                    }
                    catch (Exception ex)
                    {
                        throw new CodeFirstException("Unexpected error when initialising [NodePickerConfigAttribute] with StartNodeSpecifier.Path and input string " + (string.IsNullOrWhiteSpace(_startNodeInput) ? "[null or empty]" : _startNodeInput) + ". Affected node picker: " + memberName, ex);
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

            var requestContext = new HttpContext(new HttpRequest("", "http://localhost/", ""), new HttpResponse(null));
            var context = UmbracoContext.EnsureContext(new HttpContextWrapper(requestContext), ApplicationContext.Current);

            var helper = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current);
            IEnumerable<IPublishedContent> rootItems;

            if (_targetType.Implements<IMediaPicker>())
            {
                rootItems = helper.TypedMediaAtRoot();
            }
            else if (_targetType.Implements<IDocumentPicker>())
            {
                rootItems = helper.TypedContentAtRoot();
            }
            else //TODO proper support for members?
            {
                //TODO this is a bit dodge and won't work for members. How do start nodes even work for member pickers?
                rootItems = helper.TypedContentAtRoot().Union(helper.TypedMediaAtRoot());
            }
            
            var pieces = _startNodeInput.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            if (pieces.Length == 0)
            {
                return -1;
            }
            IPublishedContent current = rootItems.FirstOrDefault(x => x.Name == pieces[i]);
            i++;
            while (current != null && i < pieces.Length)
            {
                current = current.Children.FirstOrDefault(x => x.Name == pieces[i]);
				i++;
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