
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.Tags", "Tags", null, DataTypeDatabaseType.Ntext)]
    [BuiltInDataType]
    public class Tags : IUmbracoStringDataType, IEnumerable<string>, ICollection<string>
    {
        public List<string> Values { get; set; }

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
            Values = dbValue.Split(',').ToList();
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            string serial = "";
            foreach (var value in Values)
            {
                if (serial.Length > 0)
                {
                    serial += ",";
                }
                serial += value;
            }
            return serial;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (Values as System.Collections.IEnumerable).GetEnumerator();
        }

        public override string ToString()
        {
            return Serialise();
        }

        public string this[int index]
        {
            get
            {
                return Values[index];
            }
            set
            {
                Values[index] = value;
            }
        }

        public void Add(string item)
        {
            Values.Add(item);
        }

        public void Clear()
        {
            Values.Clear();
        }

        public bool Contains(string item)
        {
            return Values.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Values.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string item)
        {
            return Values.Remove(item);
        }
    }
}