
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
    [DataType("Umbraco.MultipleMediaPicker", "Multiple Media Picker", null, DataTypeDatabaseType.Nvarchar)]
    [BuiltInDataType]
    public class MultipleMediaPicker : IUmbracoStringDataType, ICollection<MediaItem>, IEnumerable<MediaItem>
    {
        public List<MediaItem> Items { get; set; }

        public MediaItem this[int index]
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

        public void Add(MediaItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(MediaItem item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(MediaItem[] array, int arrayIndex)
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

        public bool Remove(MediaItem item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<MediaItem> GetEnumerator()
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
        public void Initialise(string dbValue)
        {
            if (string.IsNullOrWhiteSpace(dbValue))
            {
                Items = new List<MediaItem>();
            }
            else
            {
                Items = dbValue.Split(',').Select(x => new MediaItem() { MediaNodeId = int.Parse(x) }).ToList();
            }
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            string serial = "";
            foreach (var value in Items)
            {
                if (serial.Length > 0)
                {
                    serial += ",";
                }
                serial += value.MediaNodeId.ToString();
            }
            return serial;
        }
    }
}