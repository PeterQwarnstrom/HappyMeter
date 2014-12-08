using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyMeter.Api.Modules
{
	public class MoodModule : Nancy.NancyModule
	{
		public MoodModule()
		{
			Get["/mood"] = _ => "Peter was here!";
		}
	}
}