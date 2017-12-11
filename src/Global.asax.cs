using System;
using ServiceStack;
using ServiceStack.Formats;
using Docs.Logic;

namespace Docs
{
    public class DocsAppHost : AppHostBase
    {
        public DocsAppHost() : base("ServiceStack Docs", typeof(PageService).Assembly) { }

        public override void OnBeforeInit()
        {
            Config.WebHostUrl = AppSettings.GetString("WebHostUrl");
            string filePath = Config.WebHostPhysicalPath.CombineWith("Pages.json");
            PageManager.Default.Init(filePath, Config.WebHostUrl);
        }

        public override void Configure(Funq.Container container)
        {
            container.Register(PageManager.Default);

            Plugins.Add(new MarkdownFormat { MarkdownBaseType = typeof(CustomMarkdownPage) });

            Routes
                .Add<Page>("/pages")
                .Add<Page>("/pages/{Name}")
                .Add<Category>("/category/{Name}")
                .Add<Search>("/search")
                .Add<Search>("/search/{Query}");
        }
    }

    public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			new DocsAppHost().Init();
		}
	}
}