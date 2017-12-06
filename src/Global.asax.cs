using System;

namespace Docs
{	
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			new DocsAppHost().Init();
		}
	}
}