using System;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Effects.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("dnvXamarin")]
[assembly: ExportEffect(typeof(EntryEffect), "EntryEffect")]
[assembly: ExportEffect(typeof(CircleEffect), "CircleEffect")]
[assembly: ExportEffect(typeof(CornerRadiusEffect), "CornerRadiusEffect")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace Effects.Droid
{
	public class EntryEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			
		}

		protected override void OnDetached()
		{
			
		}
	}
	public class CornerRadiusEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			throw new NotImplementedException();
		}

		protected override void OnDetached()
		{
			throw new NotImplementedException();
		}
	}
	public class ShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			throw new NotImplementedException();
		}

		protected override void OnDetached()
		{
			throw new NotImplementedException();
		}
	}

	#region CircleEffect
	public class CircleEffect : PlatformEffect
	{
		private ViewOutlineProvider _originalProvider;

		public CircleEffect()
		{
		}

		protected override void OnAttached()
		{
			this._originalProvider = base.Container.OutlineProvider;
			base.Container.OutlineProvider = new CircleEffect.CircleOutlineProvider();
			base.Container.ClipToOutline = true;
		}

		protected override void OnDetached()
		{
			base.Container.ClipToOutline = false;
			base.Container.OutlineProvider =this._originalProvider;
		}

		private class CircleOutlineProvider : ViewOutlineProvider
		{
			public CircleOutlineProvider()
			{
			}

			public override void GetOutline(Android.Views.View view, Outline outline)
			{
				double width = (double)view.Width;
				double height = (double)view.Height;
				if (width <= 0 || height <= 0)
				{
					return;
				}
				double num = Math.Min(width, height);
				float single = (float)(num / 2);
				double num1 = (width > num ? (width - num) / 2 : 0);
				double num2 = (height > num ? (height - num) / 2 : 0);
				outline.SetRoundRect(new Rect((int)num1, (int)num2, (int)(num1 + num), (int)(num2 + num)), single);
			}
		}
	}
	#endregion

}

