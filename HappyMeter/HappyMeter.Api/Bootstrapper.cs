using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HappyMeter.Api
{
	public class Bootstrapper : Nancy.DefaultNancyBootstrapper
	{
			protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
			{
				base.ConfigureApplicationContainer(container);
				container.Register<Raven.Client.IDocumentStore>(GenerateRavenDocStore());
			}

			private Raven.Client.IDocumentStore GenerateRavenDocStore()
			{
				var ravenUrl = ConfigurationManager.AppSettings["RavenUrl"];
				var ravenApiKey = ConfigurationManager.AppSettings["RavenApiKey"];
	
				var docstore = new Raven.Client.Document.DocumentStore
				{
					//ConnectionStringName = "RavenDB"
					Url = ravenUrl,
					ApiKey = ravenApiKey
				};
				docstore.Initialize();

				return docstore;
			}

			protected override void ConfigureRequestContainer(
					Nancy.TinyIoc.TinyIoCContainer container, Nancy.NancyContext context)
			{
				base.ConfigureRequestContainer(container, context);
				container.Register<Raven.Client.IDocumentSession>(GenerateRavenSession(container));
			}

			private Raven.Client.IDocumentSession GenerateRavenSession(
					Nancy.TinyIoc.TinyIoCContainer container)
			{
				var store = container.Resolve<Raven.Client.IDocumentStore>();
				var session = store.OpenSession();
				return session;
			}
	}
}
