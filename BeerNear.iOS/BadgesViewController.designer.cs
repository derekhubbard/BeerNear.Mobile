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
	[Register ("FirstViewController")]
	partial class BadgesViewController
	{
		[Outlet]
		MonoTouch.UIKit.UICollectionView badgeCollection { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (badgeCollection != null) {
				badgeCollection.Dispose ();
				badgeCollection = null;
			}
		}
	}
}
