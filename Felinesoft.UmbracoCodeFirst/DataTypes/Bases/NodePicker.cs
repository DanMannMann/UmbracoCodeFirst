using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public abstract class NodePicker<Tnode, Tnodedetails> : NodePicker, IUmbracoNtextDataType, ICollection<Tnode>, IEnumerable<Tnode>, IHtmlString, IPreValueFactory
        where Tnode : CodeFirstContentBase<Tnodedetails>
        where Tnodedetails : ContentNodeDetails
    {
        private NodeType _type;

        protected NodePicker(NodeType type)
        {
            _type = type;
            Items = new List<Tnode>();
        }

        public List<Tnode> Items { get; private set; }

        public Tnode this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        public void Add(Tnode item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(Tnode item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(Tnode[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Tnode item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<Tnode> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public virtual void Initialise(string dbValue)
        {
            Items = new List<Tnode>();
            var ids = dbValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
            foreach (var id in ids)
            {
                var model = GetModelFromId(id);
                Add(model);
            }
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public virtual string Serialise()
        {
            return string.Join(",", this.Where(x => x.NodeDetails != null && x.NodeDetails.UmbracoId > 0).Select(x => x.NodeDetails.UmbracoId.ToString()));
        }

        internal override void SetCollection(object collection)
        {
            if (collection == null)
            {
                Items = new List<Tnode>();
            }
            else
            {
                var col = collection as IEnumerable<Tnode>;
                if (col == null)
                {
                    throw new CodeFirstException("Not an enumerable, or not of the correct type of element");
                }
                Items = col.ToList();
            }
        }

        protected abstract Tnode GetModelFromId(int id);

        public virtual IDictionary<string, PreValue> GetPreValues(PreValueContext context)
        {
            return GetPreValuesInternal(context);
        }

        protected IDictionary<string, PreValue> GetPreValuesInternal(PreValueContext context, int maxNumberOverride = -1)
        {
            Dictionary<string, PreValue> result = new Dictionary<string, PreValue>();

            string typeAlias;
            try
            {
                Type target = this.GetType();
                while ((!target.IsGenericType || target.GetGenericTypeDefinition() != typeof(NodePicker<,>)) && target.BaseType != null)
                {
                    target = target.BaseType;
                }

                if (((!target.IsGenericType || target.GetGenericTypeDefinition() != typeof(NodePicker<,>)) && target.BaseType != null))
                {
                    throw new CodeFirstException("The type " + this.GetType().FullName + " does not specify a valid content type as its generic parameter");
                }

                typeAlias = target.GetGenericArguments().First().GetCodeFirstAttribute<ContentTypeAttribute>().Alias;
            }
            catch (Exception e)
            {
                throw new CodeFirstException("The type " + this.GetType().FullName + " does not specify a valid content type as its generic parameter", e);
            }

            var configAttribute = context.CurrentMember.GetCodeFirstAttribute<NodePickerConfigAttribute>();
            var startNodeString = string.Empty;
            var minNumberString = string.Empty;
            var maxNumberString = string.Empty;

            if (configAttribute != null)
            {
                if (configAttribute.StartNodeId > 0)
                {
                    startNodeString = ", \"id\": " + configAttribute.StartNodeId.ToString();

                }
                if (configAttribute.MinimumItems > -1)
                {
                    minNumberString = configAttribute.MinimumItems.ToString();
                }
                if (configAttribute.MaximumItems > -1)
                {
                    maxNumberString = configAttribute.MaximumItems.ToString();
                }
                typeAlias += "," + configAttribute.AllowedDescendantString;
            }

            if (maxNumberOverride != -1)
            {
                maxNumberString = maxNumberOverride.ToString();
            }

            result.Add("startNode", new PreValue(id: 0, value: "{\"type\": \"" + _type.ToString() + "\"" + startNodeString + "}", sortOrder: 1));
            result.Add("filter", new PreValue(id: 0, value: typeAlias, sortOrder: 2));
            result.Add("minNumber", new PreValue(id: 0, value: minNumberString, sortOrder: 3));
            result.Add("maxNumber", new PreValue(id: 0, value: maxNumberString, sortOrder: 4));
			if (configAttribute != null)
			{
				result.Add("showEditButton", new PreValue(id: 0, value: configAttribute.ShowEditButton ? "1" : "0", sortOrder: 5));
				result.Add("showOpenButton", new PreValue(id: 0, value: configAttribute.ShowOpenButton ? "1" : "0", sortOrder: 6));
				result.Add("showPathOnHover", new PreValue(id: 0, value: configAttribute.ShowPathsWhenHovering ? "1" : "0", sortOrder: 7));
			}
			else
			{
				result.Add("showEditButton", new PreValue(id: 0, value: "0", sortOrder: 5));
				result.Add("showOpenButton", new PreValue(id: 0, value: "0", sortOrder: 6));
				result.Add("showPathOnHover", new PreValue(id: 0, value: "0", sortOrder: 7));
			}
            return result;
        }

        public virtual string ToHtmlString()
        {
            if (Items.Count == 1)
            {
                var item = Items.First();
                if (item is IHtmlString)
                {
                    return (item as IHtmlString).ToHtmlString();
                }
                else
                {
                    return HttpUtility.HtmlEncode(item.ToString());
                }
            }
            else if (Items.Count > 1)
            {
                return Items.Count() + " content items selected";
            }
            else
            {
                return "No content items selected";
            }
        }

        public override string ToString()
        {
            if (Items.Count == 1)
            {
                var item = Items.First();
                return item.ToString();
            }
            else if (Items.Count > 1)
            {
                return Items.Count() + " content items selected";
            }
            else
            {
                return "No content items selected";
            }
        }
    }

    public abstract class NodePicker
    {
        internal abstract void SetCollection(object collection);
    }
}