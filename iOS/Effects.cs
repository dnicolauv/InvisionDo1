using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using Effects.iOS;

[assembly: ResolutionGroupName("InvisionDo1")]
[assembly: ExportEffect(typeof(EntryEffect), "EntryEffect")]
[assembly: ExportEffect(typeof(CircleEffect), "CircleEffect")]
[assembly: ExportEffect(typeof(CornerRadiusEffect), "CornerRadiusEffect")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace Effects.iOS
{
	#region EntryEffect
	//Removes borders of the entry control on iOS
	public class EntryEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				if (Control is UITextField)
				{
					UITextField textField = (UITextField)Control;
					textField.BorderStyle = UITextBorderStyle.None;
				}
			}
			catch (Exception)
			{
				//Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);

			try
			{
				if (Control is UITextField)
				{
					UITextField textField = (UITextField)Control;
					textField.BorderStyle = UITextBorderStyle.None;
				}
			}
			catch (Exception)
			{
				//Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}
	}
	#endregion

	#region CircleEffect
	public class CircleEffect : PlatformEffect
	{
		public CircleEffect()
		{
		}

		protected override void OnAttached()
		{
			this.UpdateCircle();
		}

		protected override void OnDetached()
		{
			base.Container.Layer.Mask = null;
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);
			if (args.PropertyName == VisualElement.WidthProperty.PropertyName || args.PropertyName == VisualElement.HeightProperty.PropertyName)
			{
				this.UpdateCircle();
			}
		}

		private void UpdateCircle()
		{
			double width = ((View)base.Element).Width;
			double height = ((View)base.Element).Height;
			if (width <= 0 || height <= 0)
			{
				return;
			}
			double num = Math.Min(width, height);
			double num1 = (width > num ? (width - num) / 2 : 0);
			double num2 = (height > num ? (height - num) / 2 : 0);
			CAShapeLayer cAShapeLayer = new CAShapeLayer();
			cAShapeLayer.Path = CGPath.EllipseFromRect(new CGRect(num1, num2, num, num));
			base.Container.Layer.Mask = cAShapeLayer;
		}
	}
	#endregion

	#region CornerRadiusEffect
	public class CornerRadiusEffect : PlatformEffect
	{
		private nfloat _originalRadius;

		public CornerRadiusEffect()
		{
		}

		protected override void OnAttached()
		{
			if (base.Container != null)
			{
				this._originalRadius = base.Container.Layer.CornerRadius;
				base.Container.ClipsToBounds = true;
				this.UpdateCorner();
			}
		}

		protected override void OnDetached()
		{
			if (base.Container != null)
			{
				base.Container.Layer.CornerRadius = this._originalRadius;
				base.Container.ClipsToBounds = false;
			}
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);

			if (args.PropertyName == "CornerRadius")
			{				
				this.UpdateCorner();
			}
		}

		private void UpdateCorner()
		{
			var effect = (InvisionDo1.Effects.CornerRadiusEffect)Element.Effects.FirstOrDefault(e => e is InvisionDo1.Effects.CornerRadiusEffect);

			if (effect != null)
				base.Container.Layer.CornerRadius = (nfloat)effect.CornerRadius;
		}
	}
	#endregion

	#region ShadowEffect
	public class ShadowEffect : PlatformEffect
	{
		private CGSize _originalOffset;

		private CGColor _originalColor;

		private float _originalOpacity;

		private nfloat _originalRadius;

		public ShadowEffect()
		{
		}

		protected override void OnAttached()
		{
			if (base.Container != null)
			{
				this._originalOffset = base.Container.Layer.ShadowOffset;
				this._originalColor = base.Container.Layer.ShadowColor;
				this._originalOpacity = base.Container.Layer.ShadowOpacity;
				this._originalRadius = base.Container.Layer.ShadowRadius;
				this.UpdateShadow();
			}
		}

		protected override void OnDetached()
		{
			if (base.Container != null)
			{
				base.Container.Layer.ShadowColor = this._originalColor;
				base.Container.Layer.ShadowOffset = this._originalOffset;
				base.Container.Layer.ShadowOpacity = this._originalOpacity;
				base.Container.Layer.ShadowRadius = this._originalRadius;
			}
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);
			if (args.PropertyName == "Shadow" || args.PropertyName == "ShadowColor" || args.PropertyName == "ShadowSize")
			{
				this.UpdateShadow();
			}
		}

		private void UpdateShadow()
		{
			var effect = (InvisionDo1.Effects.ShadowEffect)Element.Effects.FirstOrDefault(e => e is InvisionDo1.Effects.ShadowEffect);

			if (effect != null)
			{
				nfloat _nfloat;
				base.Container.Layer.ShadowOpacity = 1f;
				base.Container.Layer.ShadowColor = ColorExtensions.ToCGColor(effect.ShadowColor);
				nfloat shadowSize = (nfloat)effect.ShadowSize;
				base.Container.Layer.ShadowOffset = new CGSize(0, (shadowSize > 0 ? 1 : 0));
				CALayer layer = base.Container.Layer;
				if (shadowSize > 0)
				{
					_nfloat = shadowSize;
				}
				else
				{
					_nfloat = 0;
				}
				layer.ShadowRadius = _nfloat;
			}
		}
	}
	#endregion
}