using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Extensions to convert strings between proper, pascal and camel case
    /// </summary>
    public static class CasingExtensions
    {
        /// <summary>
        /// Convert the string to Pascal case.
        /// </summary>
        public static string ToPascalCase(this string the_string)
        {
            TextInfo info = Thread.CurrentThread.CurrentCulture.TextInfo;
            the_string = info.ToTitleCase(the_string);
            string[] parts = the_string.Split(new char[] { },
                StringSplitOptions.RemoveEmptyEntries);
            string result = String.Join(String.Empty, parts);
            return result;
        }

        /// <summary>
        /// Convert the string to camel case
        /// </summary>
        public static string ToCamelCase(this string the_string)
        {
            the_string = the_string.ToProperCase().ToPascalCase();
            return the_string.Substring(0, 1).ToLower() + the_string.Substring(1);
        }

        /// <summary>
        /// Capitalize the first character and add a space before
        /// each capitalized letter except the first character.
        /// </summary>
        /// <param name="the_string"></param>
        /// <returns></returns>
        public static string ToProperCase(this string the_string)
        {
            string result = Regex.Replace(the_string, @"(?<!^)((?<!\d)\d|(?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z]))", " $1");
            return result.Substring(0, 1).ToUpper() + result.Substring(1);
        }
    }
}
