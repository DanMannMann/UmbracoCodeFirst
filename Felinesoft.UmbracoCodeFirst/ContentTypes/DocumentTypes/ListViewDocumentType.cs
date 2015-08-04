using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Extensions;

using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    /// <summary>
    /// A base class for code-first document types with strongly-typed child collections
    /// </summary>
    /// <typeparam name="T">The type of the children</typeparam>
    public abstract class ListViewDocumentType<T> : DocumentTypeBase, IListViewDocumentType<T> where T : DocumentTypeBase
    {
        private List<T> _children;

        private void init()
        {
            if (_children == null)
            {
                _children = new List<T>();

                if (NodeDetails == null || (NodeDetails.PublishedContent == null && NodeDetails.Content == null))
                {
                    return;
                }
                
                if (NodeDetails.IsPublishedInstance)
                {
                    foreach (var child in NodeDetails.PublishedContent.Children)
                    {
                        T model;
                        if (CodeFirstManager.Current.Modules.DocumentModelModule.TryConvertToModel<T>(child, out model))
                        {
                            _children.Add(model);
                        }
                    }
                }
                else
                {
                    foreach (var child in NodeDetails.Content.Children())
                    {
                        T model;
                        if (CodeFirstManager.Current.Modules.DocumentModelModule.TryConvertToModel<T>(child, out model))
                        {
                            _children.Add(model);
                        }
                    }
                }
            }
        }

        public IEnumerable<T> Children
        {
            get
            {
                return _children;
            }
        }

        /// <summary>
        /// Gets the enumerator to iterate the child documents
        /// </summary>
        /// <returns>the enumerator to iterate the child documents</returns>
        public IEnumerator<T> GetEnumerator()
        {
            init();
            return _children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            init();
            return _children.GetEnumerator();
        }

        /// <summary>
        /// Adds a child document to the collection
        /// </summary>
        /// <param name="item">The document to add</param>
        public void Add(T item)
        {
            init();
            _children.Add(item);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            init();
            _children.Clear();
        }

        /// <summary>
        /// Returns true if the collection contains the specified item
        /// </summary>
        /// <param name="item">The item to test for</param>
        /// <returns>true if the collection contains the specified item</returns>
        public bool Contains(T item)
        {
            init();
            return _children.Contains(item);
        }

        /// <summary>
        /// Copies the items from the collection to the specified array
        /// </summary>
        /// <param name="array">The array to copy in to</param>
        /// <param name="arrayIndex">The starting index to copy in to</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            init();
            _children.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns the number of elements in the collection
        /// </summary>
        public int Count
        {
            get { init(); return _children.Count; }
        }

        /// <summary>
        /// Always returns false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the specified item from the collection
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>True if the item was found and removed</returns>
        public bool Remove(T item)
        {
            init();
            return _children.Remove(item);
        }
    }

    /// <summary>
    /// Represents a base for code-first document types with strongly-typed child collections
    /// </summary>
    /// <typeparam name="T">The type of the children</typeparam>
    public interface IListViewDocumentType<T> : IEnumerable<T>, ICollection<T> where T : DocumentTypeBase
    {

    }
}
