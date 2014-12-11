using System.Net.Mime;
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
			: base("/{area}/Moods")
		{
			_session = session;

			Get["/"] = parameter =>
			{
				string area = parameter.area;
				
				var result = _session.Query<Model.Mood>().Where(p => p.Area == area);
				return Response.AsJson(result);
			};

			Get["/{id}"] = parameter =>
			{
				string area = parameter.area;
				string id = parameter.id;

				var result = _session.Load<Model.Mood>("Moods/" + id);

				if (result == null || result.Area != area)
					return new Response { StatusCode = HttpStatusCode.NotFound };

				return Response.AsJson(result);
			};

			Post["/"] = parameter =>
					{
						string area = parameter.area;
						
						var mood = this.Bind<Model.Mood>();
						mood.Area = area;
						mood.TimeStamp = DateTime.Now;

						_session.Store(mood);
						_session.SaveChanges();

						var response = new Response
						{
							StatusCode = HttpStatusCode.Created
						};
						response.Headers["Location"] = mood.Id;

						return response;
					};
		}
	}
}