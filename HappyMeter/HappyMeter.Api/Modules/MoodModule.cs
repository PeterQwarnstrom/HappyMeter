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
            :base("/Moods")
        {
            _session = session;

            Get["/"] = _ =>
            {
                var result = _session.Query<Model.Mood>();
                return Response.AsJson(result);
            };

            Get["/{id}"] = parameter =>
            {
                string id = parameter.id;
                var result = _session.Load<Model.Mood>("Moods/" + id);

                return result == null ? new Response { StatusCode = HttpStatusCode.NotFound } : Response.AsJson(result);
            };

            Post["/"] = parameter =>
                {
                    var mood = this.Bind<Model.Mood>();
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