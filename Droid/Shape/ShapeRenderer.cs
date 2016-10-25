using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;


[assembly: ExportRenderer(typeof(InvisionDo1.ShapeView), typeof(InvisionDo1.Droid.ShapeRenderer))]
namespace InvisionDo1.Droid
{
    public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
    {
        public ShapeRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ShapeView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            SetNativeControl(new Shape(Resources.DisplayMetrics.Density, Context)
            {
                ShapeView = Element
            });
        }
    }
}