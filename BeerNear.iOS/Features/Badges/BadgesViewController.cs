using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BeerNear.iOS
{
	public partial class BadgesViewController : UITableViewController
	{
		private UntappdService _untappdService;
//		private ObservableCollection<Badge> _badges;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public BadgesViewController (IntPtr handle) : base (handle)
		{
			this._untappdService = new UntappdService ();

			this.Title = NSBundle.MainBundle.LocalizedString ("Badges", "Badges");
			this.TabBarItem.Image = UIImage.FromBundle ("first");
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		void HandleBadgesCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			// Whenever the Items change, reload the data.
			this.TableView.ReloadData ();
		}

		void HandleNetworkActivityStarted (object sender, EventArgs args)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}

		void HandleNetworkActivityEnded(object sender, EventArgs args)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			var source = new BadgesTableSource (_untappdService);
			source.BadgeDataModified += HandleBadgeDataModified;
			source.NetworkActivityStarted += HandleNetworkActivityStarted;
			source.NetworkActivityEnded += HandleNetworkActivityEnded;

			source.LoadInitialData (); // This smells.  Can we avoid initializing this here?  Is there a delegate in the UITableViewSource that we can implement?
			this.TableView.Source = source;
		}

		void HandleBadgeDataModified (object sender, EventArgs args)
		{
			this.InvokeOnMainThread (() => {
				this.TableView.ReloadData();
			});
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
	}
}

