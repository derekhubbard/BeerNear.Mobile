using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BeerNear.iOS
{
	public class BadgeTableSource : UITableViewSource
	{
		private List<Badge> _badges;

		public BadgeTableSource (List<Badge> badges)
		{
			_badges = badges;
		}

		public override int RowsInSection(UITableView tableView, int section) 
		{
			return _badges.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell ("BadgeCell");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, "BadgeCell");
			}
			Badge badge = _badges[indexPath.Row];
			cell.TextLabel.Text = badge.BadgeName;

			return cell;
		}
	}
}

