using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class CalendarViewDay : ContentView
	{

		TapGestureRecognizer tapGr;
		public CalendarViewDay(Command clickCommand)
		{
			InitializeComponent();

			BindingContext = this;

			tapGr = new TapGestureRecognizer();
			this.GestureRecognizers.Add(tapGr);
			tapGr.Command = clickCommand;
			tapGr.CommandParameter = this;
		}

		public void InitializeDay(Color _dayColor, Color _otherMonthDayColor, CurrentDayHilightType _currentDayHilightType, Color _currentDayHilightBackgroundColor,
								   EventDayMarkerType _eventDayMarkerType, Color _eventDayMarkerColor, Color _weekHeaderColor)
		{
			this.DayColor = _dayColor;
			this.OtherMonthDayColor = _otherMonthDayColor;
			this.CurrentDayHilightType = _currentDayHilightType;
			this.CurrentDayHilightBackgroundColor = _currentDayHilightBackgroundColor;
			this.EventDayMarker = _eventDayMarkerType;
			this.EventDayMarkerColor = _eventDayMarkerColor;
			this.WeekHeaderColor = _weekHeaderColor;
		}

		#region Style props

		public static readonly BindableProperty HasEventsProperty = BindableProperty.Create<CalendarViewDay, bool>(
		   p => p.HasEvents, false, BindingMode.Default, null, HasEventsPropertyChanged);
		public bool HasEvents
		{
			get { return (bool)GetValue(HasEventsProperty); }
			set { SetValue(HasEventsProperty, value); }
		}
		private static void HasEventsPropertyChanged(BindableObject bindable, bool oldValue, bool newValue)
		{
			var day = (bindable as CalendarViewDay);
			day.HasEvents = newValue;
			day.indicator.IsVisible = newValue;
			day.indicator.BackgroundColor = day.EventDayMarkerColor;

		}

		public static readonly BindableProperty VisibleProperty = BindableProperty.Create<CalendarViewDay, bool>(
		   p => p.Visible, true, BindingMode.Default, null, VisiblePropertyChanged);
		public bool Visible
		{
			get { return (bool)GetValue(VisibleProperty); }
			set { SetValue(VisibleProperty, value); }
		}
		private static void VisiblePropertyChanged(BindableObject bindable, bool oldValue, bool newValue)
		{
			(bindable as CalendarViewDay).Visible = newValue;
		}


		public static readonly BindableProperty CurrentDayHilightTypeProperty = BindableProperty.Create<CalendarViewDay, CurrentDayHilightType>(
			p => p.CurrentDayHilightType, CurrentDayHilightType.CircularBackground, BindingMode.Default, null, CurrentDayHilightTypePropertyChanged);
		public CurrentDayHilightType CurrentDayHilightType
		{
			get { return (CurrentDayHilightType)GetValue(CurrentDayHilightTypeProperty); }
			set { SetValue(CurrentDayHilightTypeProperty, value); }
		}
		private static void CurrentDayHilightTypePropertyChanged(BindableObject bindable, CurrentDayHilightType oldValue, CurrentDayHilightType newValue)
		{
			(bindable as CalendarViewDay).CurrentDayHilightType = newValue;
		}


		public static readonly BindableProperty CurrentDayHilightBackgroundColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.CurrentDayHilightBackgroundColor, Color.Gray, BindingMode.Default, null, CurrentDayHilightBackgroundColorPropertyChanged);
		public Color CurrentDayHilightBackgroundColor
		{
			get { return (Color)GetValue(CurrentDayHilightBackgroundColorProperty); }
			set { SetValue(CurrentDayHilightBackgroundColorProperty, value); }
		}
		private static void CurrentDayHilightBackgroundColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).CurrentDayHilightBackgroundColor = newValue;
		}


		public static readonly BindableProperty EventDayMarkerProperty = BindableProperty.Create<CalendarViewDay, EventDayMarkerType>(
		   p => p.EventDayMarker, EventDayMarkerType.Dot, BindingMode.Default, null, EventDayMarkerPropertyChanged);
		public EventDayMarkerType EventDayMarker
		{
			get { return (EventDayMarkerType)GetValue(EventDayMarkerProperty); }
			set { SetValue(EventDayMarkerProperty, value); }
		}
		private static void EventDayMarkerPropertyChanged(BindableObject bindable, EventDayMarkerType oldValue, EventDayMarkerType newValue)
		{
			(bindable as CalendarViewDay).EventDayMarker = newValue;
		}


		public static readonly BindableProperty EventDayMarkerColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.EventDayMarkerColor, Color.Default, BindingMode.Default, null, EventDayMarkerColorPropertyChanged);
		public Color EventDayMarkerColor
		{
			get { return (Color)GetValue(EventDayMarkerColorProperty); }
			set { SetValue(EventDayMarkerColorProperty, value); }
		}
		private static void EventDayMarkerColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).EventDayMarkerColor = newValue;
		}


		public static readonly BindableProperty DayColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.DayColor, Color.Default, BindingMode.Default, null, DayColorPropertyChanged);
		public Color DayColor
		{
			get { return (Color)GetValue(DayColorProperty); }
			set { SetValue(DayColorProperty, value); }
		}
		private static void DayColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).DayColor = newValue;
		}


		public static readonly BindableProperty WeekHeaderColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.WeekHeaderColor, Color.Default, BindingMode.Default, null, WeekHeaderColorPropertyChanged);
		public Color WeekHeaderColor
		{
			get { return (Color)GetValue(WeekHeaderColorProperty); }
			set { SetValue(WeekHeaderColorProperty, value); }
		}
		private static void WeekHeaderColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).WeekHeaderColor = newValue;
		}


		public static readonly BindableProperty OtherMonthDayColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.OtherMonthDayColor, Color.Gray, BindingMode.Default, null, OtherMonthDayColorPropertyChanged);
		public Color OtherMonthDayColor
		{
			get { return (Color)GetValue(OtherMonthDayColorProperty); }
			set { SetValue(OtherMonthDayColorProperty, value); }
		}
		private static void OtherMonthDayColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).OtherMonthDayColor = newValue;
		}


		public static readonly BindableProperty IsCurrentMonthProperty = BindableProperty.Create<CalendarViewDay, bool>(
			p => p.IsCurrentMonth, false, BindingMode.Default, null, IsCurrentMonthPropertyChanged);
		public bool IsCurrentMonth
		{
			get { return (bool)GetValue(IsCurrentMonthProperty); }
			set { SetValue(IsCurrentMonthProperty, value); }
		}
		private static void IsCurrentMonthPropertyChanged(BindableObject bindable, bool oldValue, bool newValue)
		{
			var day = (bindable as CalendarViewDay);
			day.IsCurrentMonth = newValue;
			day.CaculatedTextColor = day.IsCurrentMonth ? day.DayColor : day.OtherMonthDayColor;
		}


		public static readonly BindableProperty CaculatedTextColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.CaculatedTextColor, Color.Gray, BindingMode.TwoWay, null, CaculatedTextColorPropertyChanged);
		public Color CaculatedTextColor
		{
			get { return (Color)GetValue(CaculatedTextColorProperty); }
			set { SetValue(CaculatedTextColorProperty, value); }
		}
		private static void CaculatedTextColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).CaculatedTextColor = newValue;
		}


		public static readonly BindableProperty CaculatedBackgroundColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
			p => p.CaculatedBackgroundColor, Color.Transparent, BindingMode.Default, null, CaculatedBackgroundColorPropertyChanged);
		public Color CaculatedBackgroundColor
		{
			get { return (Color)GetValue(CaculatedBackgroundColorProperty); }
			set { SetValue(CaculatedBackgroundColorProperty, value); }
		}
		private static void CaculatedBackgroundColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarViewDay).CaculatedBackgroundColor = newValue;
			(bindable as CalendarViewDay).background.BackgroundColor = newValue;
		}


		public static readonly BindableProperty DayProperty = BindableProperty.Create<CalendarViewDay, DateTime>(
			p => p.Day, DateTime.MinValue, BindingMode.Default, null, DayPropertyChanged);
		public DateTime Day
		{
			get { return (DateTime)GetValue(DayProperty); }
			set { SetValue(DayProperty, value); }
		}
		private static void DayPropertyChanged(BindableObject bindable, DateTime oldValue, DateTime newValue)
		{
			var day = (bindable as CalendarViewDay);
			day.Day = newValue;
			day.CaculatedBackgroundColor = newValue == DateTime.Now.Date ? day.CurrentDayHilightBackgroundColor : day.BackgroundColor;
		}


		public static readonly BindableProperty CalculatedIndicatorColorProperty = BindableProperty.Create<CalendarViewDay, Color>(
		   p => p.CalculatedIndicatorColor, Color.Default, BindingMode.Default, null, CalculatedIndicatorColorPropertyChanged);
		public Color CalculatedIndicatorColor
		{
			get { return (Color)GetValue(CalculatedIndicatorColorProperty); }
			set { SetValue(CalculatedIndicatorColorProperty, value); }
		}
		private static void CalculatedIndicatorColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			var day = (bindable as CalendarViewDay);
			day.CalculatedIndicatorColor = newValue;
		}


		public static readonly BindableProperty CalculatedFontAttributesProperty = BindableProperty.Create<CalendarViewDay, FontAttributes>(
		   p => p.CalculatedFontAttributes, FontAttributes.None, BindingMode.Default, null, CalculatedFontAttributesPropertyChanged);
		public FontAttributes CalculatedFontAttributes
		{
			get { return (FontAttributes)GetValue(CalculatedFontAttributesProperty); }
			set { SetValue(CalculatedFontAttributesProperty, value); }
		}
		private static void CalculatedFontAttributesPropertyChanged(BindableObject bindable, FontAttributes oldValue, FontAttributes newValue)
		{
			var day = (bindable as CalendarViewDay);
			day.CalculatedFontAttributes = newValue;
		}
		#endregion
	}

	internal class DayNameValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime && targetType == typeof(string))
				return ((DateTime)value).Day.ToString();
			return "N/a";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
