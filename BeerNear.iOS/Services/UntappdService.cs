using System;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BeerNear.iOS
{
	public class UntappdService
	{
		private static RestClient Client = new RestClient(BASE_URI);

		private const string BASE_URI = "http://api.untappd.com/v4";

		//TODO: Move this to a separate file and exclude from source control.
		private const string CLIENT_SECRET = "DCDBF0B32A3F5AFC37DA5D6BD7FC8211DB4726DF";
		private const string CLIENT_ID = "DABE2BA1D4FE9F51C1C055E7EE9EA90331233BE2";

		public UntappdService ()
		{
		}

		public string GetUserInfo(string userName) 
		{
			// TODO: Move url building to additional resource.
			string uri = string.Format("/user/info/{0}?client_secret={1}&client_id={2}", userName, CLIENT_SECRET, CLIENT_ID);
			var request = new RestRequest (uri, Method.GET);

			var response = Client.Execute (request);
			var content = response.Content;
			return content;
		}

		public List<Badge> GetUserBadges(string userName)
		{
			// TODO: Move url building to additional resource.
			string uri = string.Format("/user/badges/{0}?client_secret={1}&client_id={2}", userName, CLIENT_SECRET, CLIENT_ID);
			var request = new RestRequest (uri, Method.GET);

			var response = Client.Execute (request);
			var content = response.Content;

			// TOD: Pull this out into a separate resource - this method does WAY too much.
			JObject obj = JObject.Parse (content);
			List<Badge> badges = obj ["response"] ["items"].Select (x => new Badge()
			{ 
				BadgeId = (int)x["badge_id"],
				BadgeName = (string)x["badge_name"],
				BadgeDescription = (string)x["badge_description"]
			}).ToList();

			return badges;
		}

		public void GetUserBadgesAsync(string userName, Action<List<Badge>> callback)
		{
			// TODO: Move url building to additional resource.
			string uri = string.Format ("/user/badges/{0}?client_secret={1}&client_id={2}", userName, CLIENT_SECRET, CLIENT_ID);
			var request = new RestRequest (uri, Method.GET);

			Client.ExecuteAsync(request, (response) => {
				var content = response.Content;

				// TODO: Pull this out into a separate resource - this method does WAY too much.
				JObject obj = JObject.Parse (content);
				List<Badge> badges = obj ["response"] ["items"].Select (x => new Badge()
				                                                        { 
					BadgeId = (int)x["badge_id"],
					BadgeName = (string)x["badge_name"],
					BadgeDescription = (string)x["badge_description"]
				}).ToList();

				callback (badges);
			});
		}
	}
}

