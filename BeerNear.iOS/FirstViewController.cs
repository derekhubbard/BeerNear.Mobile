using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Text;

namespace BeerNear.iOS
{
	public partial class FirstViewController : UIViewController
	{
		private UntappdService _untappdService;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public FirstViewController (IntPtr handle) : base (handle)
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("First", "First");
			this.TabBarItem.Image = UIImage.FromBundle ("first");

			this._untappdService = new UntappdService ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
//			this.txtOutput.Text = "Hello world!";
//			this.btnLoadBeer.TouchUpInside += (sender, e) => {
//				this.txtOutput.Text = this._untappdService.GetUserBadges("derekhubbard");
//			};

			// TODO: Blocking - evil.  Can we move this off UI thread?
			var badges = this._untappdService.GetUserBadges ("derekhubbard");
			var sb = new StringBuilder ();
			foreach (var badge in badges) {
				sb.AppendLine (badge.BadgeName);
			}
			this.txtOutput.Text = sb.ToString ();
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

