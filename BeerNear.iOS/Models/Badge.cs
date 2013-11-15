using System;
using Newtonsoft.Json;
using MonoTouch.UIKit;

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

		public string BadgeImageSmallUrl { get; set; }

		public string BadgeImageMediumUrl { get; set; }

		public byte[] BadgeImageMedium { get; set; }

		public string BadgeImageLargeUrl { get; set; }
	}
}

