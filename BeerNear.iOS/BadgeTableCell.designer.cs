// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeerNear.iOS
{
	[Register ("BadgeTableCell")]
	partial class BadgeTableCell
	{
		[Outlet]
		MonoTouch.UIKit.UITextView BadgeDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView BadgeImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel BadgeName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BadgeName != null) {
				BadgeName.Dispose ();
				BadgeName = null;
			}

			if (BadgeDescription != null) {
				BadgeDescription.Dispose ();
				BadgeDescription = null;
			}

			if (BadgeImage != null) {
				BadgeImage.Dispose ();
				BadgeImage = null;
			}
		}
	}
}
