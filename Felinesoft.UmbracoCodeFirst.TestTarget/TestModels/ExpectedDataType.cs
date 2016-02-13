using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TestModels
{
    public class ExpectedDataType
    {
        public string PropertyEditorAlias { get; set; }
        public string DataTypeName { get; set; }
        public DataTypeDatabaseType DbType { get; set; }
    }
}