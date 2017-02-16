using System;
#if __ANDROID__
using Android.Graphics;
using Android.OS;
using Android.Views;
using Xamarin.Forms.Platform.Android;
#endif
using Xamarin.Forms;


[assembly: ResolutionGroupName("InvisionDo1")]
//[assembly: ExportEffect(typeof(EntryEffect), "EntryEffect")]
[assembly: ExportEffect(typeof(InvisionDo1.Effects.CircleEffect), "CircleEffect")]
//[assembly: ExportEffect(typeof(CornerRadiusEffect), "CornerRadiusEffect")]
//[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace Shared.Effects
{
    //public class EntryEffect : PlatformEffect
    //{
    //    protected override void OnAttached()
    //    {

    //    }

    //    protected override void OnDetached()
    //    {

    //    }
    //}
    //public class CornerRadiusEffect : PlatformEffect
    //{
    //    protected override void OnAttached()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override void OnDetached()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class ShadowEffect : PlatformEffect
    //{
    //    protected override void OnAttached()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override void OnDetached()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    #region CircleEffect
#if __ANDROID__
    public class CircleEffect : Xamarin.Forms.Platform.Android.PlatformEffect
    {

        private ViewOutlineProvider _originalProvider;

        public CircleEffect()
        {
        }

        protected bool CanBeApplied()
        {
            if (base.Container == null)
            {
                return false;
            }
            return (int)Build.VERSION.SdkInt >= 21;
        }

        protected override void OnAttached()
        {
            this._originalProvider = base.Container.OutlineProvider;
            base.Container.OutlineProvider = new CircleEffect.CirlceOutlineProvider();
            base.Container.ClipToOutline = true;
        }

        protected override void OnDetached()
        {
            base.Container.ClipToOutline = false;
            base.Container.OutlineProvider = this._originalProvider;
        }

        private class CirlceOutlineProvider : ViewOutlineProvider
        {
            public CirlceOutlineProvider()
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
#elif __IOS__
     public class CircleEffect : Xamarin.Forms.iOS.PlatformEffect
    {
    }
#endif

    #endregion

}

