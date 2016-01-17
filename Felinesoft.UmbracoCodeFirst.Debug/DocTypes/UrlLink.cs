using System;
using System.Linq;
using System.Collections.Generic;
using RJP.MultiUrlPicker.Models;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	public class UrlLink
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Target { get; set; }

		public LinkType Type { get; set; }

		public string Url { get; set; }
	}
}