using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Content.Interfaces
{
    /// <summary>
    /// <para>Defines an initialise method which the content initialiser can call to initialise values for default documents.</para>
    /// </summary>
    public interface IAutoContent
    {
        /// <summary>
        /// Initialises the current strongly-typed document instance with default values.
        /// </summary>
        void InitialiseDefaults();
    }
}
