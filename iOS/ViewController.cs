using System;
using System.Collections.Generic;
using Mondido.Configuration;
using Mondido.Payment;
using Mondido.Utils;
using MondidoCrossPlatformSDK;
using MondidoCrossPlatformSDK.Payment;
using MondidoCrossPlatformSDK.Request;
using UIKit;
namespace Demo.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate
			{
				var title = string.Format("{0} clicks!", count++);
				Button.SetTitle(title, UIControlState.Normal);
			};



		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
