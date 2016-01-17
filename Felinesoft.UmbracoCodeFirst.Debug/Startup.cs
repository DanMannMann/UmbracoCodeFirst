using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace Felinesoft.UmbracoCodeFirst.Debug
{
	public class StartUp : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			CodeFirstManager.Current.Initialise(GetType().Assembly);
		}
	}
}
