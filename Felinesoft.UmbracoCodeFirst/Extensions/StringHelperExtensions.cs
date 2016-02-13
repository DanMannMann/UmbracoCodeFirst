using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using System.Reflection;
using System.ComponentModel;
using Umbraco.Web;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System.Text.RegularExpressions;
using System.Globalization;

using Felinesoft.UmbracoCodeFirst.Attributes;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Convenience methods used when formatting and transforming Umbraco values to
    /// valid C# member names.
    /// </summary>
    public static class StringHelperExtensions
    {
        /// <summary>
        /// Function to parse an URL to a better format
        /// </summary>
        /// <param name="input">The URL that needs to be parsed</param>
        /// <param name="allowCommas">True to allow commas in the output</param>
        /// <param name="toLowerCase">True to convert the URL to lower case</param>
        /// <returns>A reformatted URL</returns>
        public static string ParseUrl(string input, bool toLowerCase = true, bool allowCommas = false)
        {
            input = input.Replace(" ", "-");
            input = input.Replace("é", "e");
            input = input.Replace("è", "e");
            input = input.Replace("à", "a");
            input = input.Replace("â", "a");
            input = input.Replace("ç", "c");
            input = input.Replace("î", "i");
            input = input.Replace("ï", "i");
            input = input.Replace("ë", "e");
            input = input.Replace("Ë", "e");
            input = input.Replace("ô", "o");
            input = input.Replace("ù", "u");
            string returnValue = "";

            for (int i = 0; i < input.Length; i++)
            {
                string s = input.Substring(i, 1);
                string regexString = "";

                if (allowCommas)
                {
                    regexString = "[A-z0-9,/\\-\\(\\)_]";
                }
                else
                {
                    regexString = "[A-z0-9/\\-\\(\\)_]";
                }

                Regex reg = new Regex(regexString);
                if (reg.IsMatch(s))
                    returnValue += s;
            }

            // If multiple spaces (-), clear to 1
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[\\-]{2,}", options);
            returnValue = regex.Replace(returnValue, @"-");

            // In case of all bad characters
            if (returnValue == "")
            {
                returnValue = "empty";
            }

            // Lowercase urls better for SEO
            if (toLowerCase)
            {
                return returnValue.ToLower();
            }

            return returnValue;
        }

        /// <summary>
        /// Replace hyphen ('-') with underscore ('_')
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HyphenToUnderscore(string input)
        {
            return input.Replace('-', '_');
        }
    }
}
