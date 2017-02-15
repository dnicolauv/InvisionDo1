using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;


[assembly: ExportRenderer(typeof(InvisionDo1.ShapeView), typeof(dnvXamarinForms.Droid.ShapeRenderer))]
namespace dnvXamarinForms.Droid
{
    public class ShapeRenderer : ViewRenderer<InvisionDo1.ShapeView, Shape>
    {
        public ShapeRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<InvisionDo1.ShapeView> e)
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