using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using BuiltIn = Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet2
{
    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.DateTime)]
    public class CustomDataType1 : IUmbracoDateDataType
    {
        public void Initialise(DateTime dbValue)
        {
            throw new System.NotImplementedException();
        }

        public DateTime Serialise()
        {
            throw new System.NotImplementedException();
        }
    }

    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.Textbox)]
    public class CustomDataType2 : IUmbracoNvarcharDataType
    {
        public void Initialise(string dbValue)
        {
            throw new System.NotImplementedException();
        }

        public string Serialise()
        {
            throw new System.NotImplementedException();
        }
    }

    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.Grid, name: "Overidden name", converterType: typeof(FakeConverter), dbType: DatabaseType.Ntext)]
    public class CustomDataType3
    {
        public void Initialise(string dbValue)
        {
            throw new System.NotImplementedException();
        }

        public string Serialise()
        {
            throw new System.NotImplementedException();
        }
    }

    public class FakeConverter : IDataTypeConverter<string, CustomDataType3>
    {
        public CustomDataType3 Create(string input, Action<object> registerContext = null)
        {
            throw new NotImplementedException();
        }

        public string Serialise(CustomDataType3 input)
        {
            throw new NotImplementedException();
        }

        public object Create(object input, Action<object> registerContext = null)
        {
            throw new NotImplementedException();
        }

        public object Serialise(object input)
        {
            throw new NotImplementedException();
        }
    }

    [DataType]
    public class InheritingDataType1 : BuiltIn.Numeric { }

    [DataType]
    public class InheritingDataType2 : BuiltIn.TrueFalse { }

    [DataType]
    public class InheritingDataType3 : BuiltIn.DocumentPicker<Child1> { }
}