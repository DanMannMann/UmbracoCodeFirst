using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataTypeInstanceAttribute : CodeFirstAttribute
    {
        public DataTypeInstanceAttribute(string propertyEditorAlias = null, string name = null, Type converterType = null, DatabaseType dbType = DatabaseType.None)
        {
            Name = name;
            PropertyEditorAlias = propertyEditorAlias;
            ConverterType = converterType;
            DbType = dbType;
        }

        /// <summary>
        /// The instance name of the data type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The property editor alias of the data type
        /// </summary>
        public string PropertyEditorAlias { get; set; }

        /// <summary>
        /// The database storage type
        /// </summary>
        public DatabaseType DbType { get; set; }

        /// <summary>
        /// The default converter type which can convert between the code-first class and an Umbraco property of that data type
        /// </summary>
        public Type ConverterType { get; set; }

        public bool HasNullProperties
        {
            get
            {
                return Name == null || PropertyEditorAlias == null || ConverterType == null || DbType == DatabaseType.None;
            }
        }
    }
}