using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType(BuiltInPropertyEditorAliases.MultipleTextstring, BuiltInDataTypes.MultipleTextstring)]
    [BuiltInDataType]
    public class MultipleTextstring : IEnumerable<string>, IList<string>, IUmbracoNtextDataType
    {
        private List<string> _collection = new List<string>();

        public void Initialise(string dbValue)
        {
            _collection = dbValue.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        public string Serialise()
        {
            return string.Join(Environment.NewLine, _collection);
        }

        public override string ToString()
        {
            return Serialise();
        }

        #region IEnumerable
        public IEnumerator<string> GetEnumerator()
        {
            return (_collection as IEnumerable<string>).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (_collection as System.Collections.IEnumerable).GetEnumerator();
        }
        #endregion

        #region IList
        public int IndexOf(string item)
        {
            return _collection.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            _collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        public string this[int index]
        {
            get
            {
                return _collection[index];
            }
            set
            {
                _collection[index] = value;
            }
        }

        public void Add(string item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(string item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string item)
        {
            return _collection.Remove(item);
        }
        #endregion
    }
}
