using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class ProgressView : ContentView
	{
		public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create<ProgressView, Color>(
			p => p.IndicatorColor, Color.Default, BindingMode.Default, null, IndicatorColorPropertyChanged);
		public Color IndicatorColor
		{
			get { return (Color)GetValue(IndicatorColorProperty); }
			set { SetValue(IndicatorColorProperty, value); }
		}

		private static void IndicatorColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			var view = (bindable as ProgressView);
			if (view != null && view.shape != null)
			{
				view.IndicatorColor = newValue;
				view.shape.IndicatorColor = newValue;
			}
		}

		public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create<ProgressView, Color>(
			p => p.StrokeColor, Color.Default, BindingMode.Default, null, StrokeColorPropertyChanged);
		public Color StrokeColor
		{
			get { return (Color)GetValue(StrokeColorProperty); }
			set { SetValue(StrokeColorProperty, value); }
		}

		private static void StrokeColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			var view = (bindable as ProgressView);
			if (view != null && view.shape != null)
			{
				view.StrokeColor = newValue;
				view.shape.StrokeColor = newValue;
			}
		}

		public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create<ProgressView, double>(
			p => p.StrokeWidth, 3, BindingMode.Default, null, StrokeWidthPropertyChanged);
		public double StrokeWidth
		{
			get { return (double)GetValue(StrokeWidthProperty); }
			set { SetValue(StrokeWidthProperty, value); }
		}

		private static void StrokeWidthPropertyChanged(BindableObject bindable, double oldValue, double newValue)
		{
			var view = (bindable as ProgressView);
			if (view != null && view.shape != null)
			{
				view.StrokeWidth = newValue;
				view.shape.StrokeWidth = (float)newValue;
			}
		}

		public static readonly BindableProperty PercentageProperty = BindableProperty.Create<ProgressView, double>(
			p => p.Percentage, 0, BindingMode.Default, null, PercentagePropertyChanged);
		public double Percentage
		{
			get { return (double)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value); }
		}

		private static void PercentagePropertyChanged(BindableObject bindable, double oldValue, double newValue)
		{
			var view = (bindable as ProgressView);
			if (view != null && view.shape != null)
			{
				view.Percentage = newValue;
				view.shape.IndicatorPercentage = (float)newValue;
				view.txt.Text = newValue.ToString("##");
			}
		}

		public ProgressView()
		{
			InitializeComponent();

			this.BindingContext = this;
		}

		async protected override void OnParentSet()
		{
			base.OnParentSet();

			await Task.Delay(200 + (int)this.Scale * 20);

			txt.Animate("text", new Animation(callback: d => this.Percentage = d,
				   start: 0,
				   end: this.Percentage,
			       easing: Easing.SinOut), 16, 1000, null, null, null);
		}

		public async void Animate()
		{
			txt.Animate("text", new Animation(callback: d => this.Percentage = d,
				   start: 0,
				   end: this.Percentage,
				   easing: Easing.SinOut), 16, 1000, null, null, null);
		}
	}
}

