using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	public static class Ext
	{
		public static IEnumerable<string> GraphemeClusters(this string s)
		{
			var enumerator = StringInfo.GetTextElementEnumerator(s);
			while (enumerator.MoveNext())
			{
				yield return (string)enumerator.Current;
			}
		}
		public static string ReverseGraphemeClusters(this string s)
		{
			return string.Join("", s.GraphemeClusters().Reverse().ToArray());
		}
	}
}