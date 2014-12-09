using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

namespace HappyMeter.Api.Modules
{
	public class MoodModule : Nancy.NancyModule
	{
		public MoodModule() : base("/mood")
		{
			Get["/"] = _ => "Peter was here!";

		    Post["/"] = parameter =>
		    {
		        var mood = this.Bind<Models.MoodModel>();
		        mood.Id = Guid.NewGuid().ToString();
		        mood.TimeStamp = DateTime.Now;

		        var response = new Response
		        {
		            StatusCode = HttpStatusCode.Created
		        };

		        return response;
		    };
		}
	}
}