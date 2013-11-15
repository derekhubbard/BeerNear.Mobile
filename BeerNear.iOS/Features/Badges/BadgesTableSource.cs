using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BeerNear.iOS
{
	public class BadgesTableSource : UITableViewSource
	{
		public delegate void BadgeDataModifiedEventHandler (object sender, EventArgs args);

		public delegate void NetworkActivityStartedHandler (object sender, EventArgs args);

		public delegate void NetworkActivityEndedHandler(object sender, EventArgs args);

		public delegate void RowUpdatedHandler (object sender, EventArgs args);

		public event BadgeDataModifiedEventHandler BadgeDataModified;
		public event NetworkActivityStartedHandler NetworkActivityStarted;
		public event NetworkActivityEndedHandler NetworkActivityEnded;
		public event RowUpdatedHandler RowUpdated;

		private List<Badge> _badges;
		private int _sectionSize = 10;
		private int _lastIndex = 9;
		private UntappdService _service;

		public BadgesTableSource (UntappdService service)
		{
			_service = service;
		}

		public void LoadInitialData()
		{
			// Raise event to identify activity indicator should be started
			NetworkActivityStarted (this, null);

			_service.GetUserBadgesAsync ("derekhubbard", InitialBadgeDownloadComplete);
		}

		private void InitialBadgeDownloadComplete(List<Badge> badges)
		{
			this._badges = badges;

			// Download images
			this.DownloadBadgeIcons (_badges.GetRange(0, _sectionSize));

			// Raise event to notify network activity complete
			this.NetworkActivityEnded (this, null);
		}

		public override int RowsInSection(UITableView tableView, int section) 
		{
			Console.WriteLine ("Number of rows: " + this._lastIndex);
			return this._lastIndex + 1;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (this._badges == null) {
				// Return dummy cell?
				return new BadgesTableCell ();
			}

			Console.WriteLine ("Row:" + indexPath.Row);
			Console.WriteLine ("LastIndex:" + _lastIndex);

			// At the end of the table?  Get additional data.
			if (indexPath.Row == this._lastIndex) {
				Console.WriteLine ("Current row: " + indexPath.Row);
				Console.WriteLine ("Getting additional images.");
				var t = new Task(() => {
					var tmp = _lastIndex;
					_lastIndex += _sectionSize;
					DownloadBadgeIcons(_badges.GetRange(tmp + 1, _sectionSize));
				});
				t.Start ();
			}

			var cell = tableView.DequeueReusableCell ("BadgeCell") as BadgesTableCell;
			if (cell == null) {
				cell = new BadgesTableCell();
			}

			Badge badge = _badges[indexPath.Row];
			cell.Tag = indexPath.Row;
			cell.BindDataToCell (indexPath.Row, badge);

			return cell;
		}

		private void DownloadBadgeIcons(List<Badge> badges)
		{
			Console.WriteLine ("Executing task to get additional images.");

//			var badges = _badges.GetRange (startIndex, count);
			badges.ForEach (x => {
				DownloadBadgeIcon(x);
			});

			// TODO: This is temp - will reload individual cells instead of entire table.
			Console.WriteLine ("Images downloaded - raising event to reload table.");
			this.BadgeDataModified (this, null);

		}

		private void DownloadBadgeIcon(Badge b)
		{
			Console.WriteLine ("Downloading image for: " + b.BadgeId);

			var client = new GZipWebClient ();
			b.BadgeImageMedium = client.DownloadData(b.BadgeImageMediumUrl);

			// TODO: Raise event, specifying individual row that should be refreshed, rather than refreshing entire table.
			// Raise event notifying ui that image has downloaded
//			this.RowUpdated (this, new RowUpdatedEventArgs());
		}
	}

	public class RowUpdatedEventArgs : EventArgs 
	{
		public RowUpdatedEventArgs(int indexPath) : base()
		{
			this.IndexPath = indexPath;
		}

		public int IndexPath { get; set; }
	}
}

