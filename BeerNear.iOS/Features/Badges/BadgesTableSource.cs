using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BeerNear.iOS
{
	public class BadgesTableSource : UITableViewSource
	{
		private List<Badge> _badges;

		public BadgesTableSource (List<Badge> badges)
		{
			_badges = badges;
		}

		public override int RowsInSection(UITableView tableView, int section) 
		{
			return _badges.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("BadgeCell") as BadgesTableCell;
			if (cell == null) {
				cell = new BadgesTableCell();
			}

			Badge badge = _badges[indexPath.Row];
			cell.BindDataToCell (badge);

			return cell;
		}
	}
}

