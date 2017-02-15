using System;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Effects.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("InvisionDo1")]
[assembly: ExportEffect(typeof(EntryEffect), "EntryEffect")]
[assembly: ExportEffect(typeof(CircleEffect), "CircleEffect")]
[assembly: ExportEffect(typeof(CornerRadiusEffect), "CornerRadiusEffect")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace Effects.Droid
{ 
    public abstract class BaseEffect : PlatformEffect
    {
        private bool _unloaded;

        private bool _attached;

        protected bool Attached
        {
            get
            {
                return this._attached;
            }
        }

        protected BaseEffect()
        {
        }

        protected virtual bool CanBeApplied()
        {
            return true;
        }

        private void ContainerViewAttachedToWindow(object sender, Android.Views.View.ViewAttachedToWindowEventArgs e)
        {
            this._unloaded = false;
        }

        private void ContainerViewDetachedFromWindow(object sender, Android.Views.View.ViewDetachedFromWindowEventArgs e)
        {
            this._unloaded = true;
        }

        protected sealed override void OnAttached()
        {
            if (this.CanBeApplied())
            {
                this._attached = true;
                //base.Container.RemoveView(new EventHandler<View.ViewDetachedFromWindowEventArgs>(this, BaseEffect.ContainerViewDetachedFromWindow));
                //base.Container.add_ViewDetachedFromWindow(new EventHandler<View.ViewDetachedFromWindowEventArgs>(this, BaseEffect.ContainerViewDetachedFromWindow));
                //base.Container.remove_ViewAttachedToWindow(new EventHandler<View.ViewAttachedToWindowEventArgs>(this, BaseEffect.ContainerViewAttachedToWindow));
                //base.Container.add_ViewAttachedToWindow(new EventHandler<View.ViewAttachedToWindowEventArgs>(this, BaseEffect.ContainerViewAttachedToWindow));
                this.OnAttachedInternal();
            }
        }

        protected virtual void OnAttachedInternal()
        {
        }

        protected sealed override void OnDetached()
        {
            if (this._attached && !this._unloaded)
            {
                this._attached = false;
                //base.get_Container().remove_ViewDetachedFromWindow(new EventHandler<View.ViewDetachedFromWindowEventArgs>(this, BaseEffect.ContainerViewDetachedFromWindow));
                //base.get_Container().remove_ViewAttachedToWindow(new EventHandler<View.ViewAttachedToWindowEventArgs>(this, BaseEffect.ContainerViewAttachedToWindow));
                this.OnDetachedInternal();
            }
        }

        protected virtual void OnDetachedInternal()
        {
        }
    }
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
	public class CircleEffect : BaseEffect
	{
        private ViewOutlineProvider _originalProvider;

        public CircleEffect()
        {
        }

        protected override bool CanBeApplied()
        {
            if (base.Container == null)
            {
                return false;
            }
            return (int)Build.VERSION.SdkInt >= 21;
        }

        protected override void OnAttachedInternal()
        {
            this._originalProvider = base.Container.OutlineProvider;
            base.Container.OutlineProvider = new CircleEffect.CirlceOutlineProvider();
            base.Container.ClipToOutline = true;
        }

        protected override void OnDetachedInternal()
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
	#endregion

}

