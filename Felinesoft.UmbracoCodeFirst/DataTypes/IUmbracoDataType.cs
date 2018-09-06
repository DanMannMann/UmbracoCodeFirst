using Marsman.UmbracoCodeFirst.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.DataTypes
{
    /// <summary>
    /// A common base for interfaces specifying valid Umbraco database storage formats
    /// </summary>
    public interface IUmbracoDataType 
    {

    }

    /// <summary>
    /// A generic base for interfaces specifying valid Umbraco database storage formats
    /// </summary>
    public interface IUmbracoDataType<T> : IUmbracoDataType
    {
        /// <summary>
        /// Initialises the current instance with the values deserialised from dbValue
        /// </summary>
        /// <param name="dbValue">The serialised value</param>
        void Initialise(T dbValue);

        /// <summary>
        /// Serialises the current instance to type T
        /// </summary>
        /// <returns>The serialised instance</returns>
        T Serialise();
    }

    /// <summary>
    /// Interface specifying an ntext database storage type
    /// </summary>
    public interface IUmbracoNtextDataType<T> : IUmbracoDataType<T>
    {

    }

    /// <summary>
    /// Interface specifying an nvarchar database storage type
    /// </summary>
    public interface IUmbracoNvarcharDataType<T> : IUmbracoDataType<T>
    {

    }

    /// <summary>
    /// Interface specifying a DateTime database storage type
    /// </summary>
    public interface IUmbracoDateTimeDataType<T> : IUmbracoDataType<T>
    {

    }

    /// <summary>
    /// Interface specifying an integer database storage type
    /// </summary>
    public interface IUmbracoIntegerDataType<T> : IUmbracoDataType<T>
    {

    }

    /// <summary>
    /// Interface specifying an integer database storage type
    /// </summary>
    public interface IUmbracoIntegerDataType : IUmbracoIntegerDataType<int>
    {

    }

     /// <summary>
    /// Interface specifying a string database storage type. This interface uses
    /// the ntext database storage type.
    /// </summary>
    public interface IUmbracoNtextDataType : IUmbracoNtextDataType<string>
    {

    }

    /// <summary>
    /// Interface specifying a string database storage type. This interface uses
    /// the ntext database storage type.
    /// </summary>
    public interface IUmbracoNvarcharDataType : IUmbracoNvarcharDataType<string>
    {

    }

     /// <summary>
    /// Interface specifying a DateTime database storage type
    /// </summary>
    public interface IUmbracoDateDataType : IUmbracoDateTimeDataType<DateTime>
    {

    }
}
