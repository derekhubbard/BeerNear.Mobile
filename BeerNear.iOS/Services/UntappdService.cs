using System;
using RestSharp;

namespace BeerNear.iOS
{
	public class UntappdService
	{
		public UntappdService ()
		{
		}

		//TODO: Clean up method naming.
		public string GetStuff() 
		{
			// TODO: Clean up implementation.
			var client = new RestClient ("http://www.google.com");

			var request = new RestRequest ("/", Method.GET);

			var response = client.Execute (request);
			var content = response.Content;
			return content;
		}
	}
}

