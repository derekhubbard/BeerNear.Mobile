using System;
using Newtonsoft.Json;

namespace BeerNear.iOS
{
	public class Badge
	{
		[JsonProperty(PropertyName = "badge_id")]
		public int BadgeId { get; set; }

		[JsonProperty(PropertyName = "badge_name")]
		public string BadgeName { get; set; }

		[JsonProperty(PropertyName = "badge_description")]
		public string BadgeDescription { get; set; }
	}
}

