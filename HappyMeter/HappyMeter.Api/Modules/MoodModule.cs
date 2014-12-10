using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

namespace HappyMeter.Api.Modules
{
	public class MoodModule : NancyModule
	{
		private Raven.Client.IDocumentSession _session;

		public MoodModule(Raven.Client.IDocumentSession session)
			: base("/mood")
		{
			_session = session;

			Get["/"] = _ =>
			{
				return "Hej"; // documentSession.Load<Model.MoodModel>();
			};

			Post["/"] = parameter =>
				{
					var mood = this.Bind<Model.MoodModel>();
					mood.Id = Guid.NewGuid().ToString();
					mood.TimeStamp = DateTime.Now;

					_session.Store(mood);
					_session.SaveChanges();

					var response = new Response
					{
						StatusCode = HttpStatusCode.Created,
					};

					return response;
				};
		}
	}
}