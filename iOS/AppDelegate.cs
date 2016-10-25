using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace InvisionDo1.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			#if GORILLA
				LoadApplication(UXDivers.Gorilla.iOS.Player.CreateApplication(new UXDivers.Gorilla.Config("Good gorilla")));
			#else
				LoadApplication(new App());
			#endif

			return base.FinishedLaunching(app, options);
		}
	}
}

