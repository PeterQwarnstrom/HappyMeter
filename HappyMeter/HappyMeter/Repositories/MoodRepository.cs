using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Repositories
{
	public class MoodRepository
	{
		private DocumentStore documentStore;

		public MoodRepository()
		{
			documentStore = new DocumentStore
			{
				ConnectionStringName = "RavenDB"
			};

			documentStore.Initialize();

		}
		public void AddMood(Models.MoodModel mood)
		{
			using (var session = documentStore.OpenSession())
			{
				session.Store(mood);

				session.SaveChanges();
			}
		}
	}
}
