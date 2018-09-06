using Newtonsoft.Json;
using System.Collections.Generic;

namespace Marsman.UmbracoCodeFirst.DataTypes
{
    public abstract class UmbracoJsonCollectionDataType<T> : UmbracoJsonDataType, IEnumerable<T>, ICollection<T>
    {
        public UmbracoJsonCollectionDataType()
            : base(NullValueHandling.Ignore)
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }

        [JsonIgnore]
        public T this[int index]
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

        public void Add(T item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        [JsonIgnore]
        public int Count
        {
            get { return Items.Count; }
        }

        [JsonIgnore]
        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}