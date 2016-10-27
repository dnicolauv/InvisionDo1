using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace InvisionDo1
{
	public enum CalendarModeType { Week = 7, Month = 42 }
	public enum CurrentDayHilightType { CircularBackground, SquareBackground, RoundedBackground, Bold }
	public enum EventDayMarkerType { Dot, Underline }

	public partial class CalendarView : ContentView
	{		
		public CalendarView()
		{
			InitializeComponent();

			BindingContext = this;

			InitializeCalendar();
		}


		public void InitializeCalendar(ObservableCollection<DateTime> initialSpecialDays = null)
		{
			if (initialSpecialDays != null)
				SpecialDays = initialSpecialDays;

			try
			{
				SpecialDays.Add(DateTime.Now);
				SpecialDays.Add(DateTime.Now.AddDays(3));
			}
			catch (Exception ex)
			{
				throw ex;
			}

			clickDayCommand = new Command((o) =>
			{
				SelectedDate = ((o as CalendarViewDay).BindingContext as CalendarViewDay).Day;
				if (OnDayClick != null)
					OnDayClick.Invoke((o as CalendarViewDay), ((o as CalendarViewDay).BindingContext as CalendarViewDay).Day);
			});


			//DAY NAMES HEADER
			var dtf = CultureInfo.CurrentUICulture.DateTimeFormat;
			for (var i = 0; i < 7; i++)
			{
				var relDay = (int)dtf.FirstDayOfWeek + i;
				var realDay = (DayOfWeek)(relDay % 7);
				var daylabel = new Label
				{
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Text = dtf.GetAbbreviatedDayName(realDay).ToUpper(),
					FontAttributes = FontAttributes.Bold,
					TextColor = WeekDayColor

				};
				gridDias.Children.Add(daylabel, i, 0);
			}

			//DAYS
			for (var i = 0; i < (int)this.CalendarMode; i++)
			{
				var diaCelda = new CalendarViewDay(clickDayCommand);

				diaCelda.InitializeDay(this.DayColor, this.OtherMonthDayColor, this.CurrentDateHilight, this.CurrentDateHilightBackgroundColor,
										this.EventDayMarker, this.EventDayMarkerColor, this.WeekDayColor);
				DayData.Add(diaCelda);
				diaCelda.BindingContext = DayData[i];
				diaCelda.HeightRequest = this.CalendarRowHeight;
				diaCelda.HorizontalOptions = LayoutOptions.Center;
				diaCelda.VerticalOptions = LayoutOptions.Center;
				gridDias.Children.Add(diaCelda, i % 7, 1 + (i / 7));
			}

			UpdateDayData();
		}

		private void UpdateDayData()
		{

			var dtf = CultureInfo.CurrentUICulture.DateTimeFormat;

			var firstDayOfMonth = this.SelectedDate.AddDays(-(this.SelectedDate.Day - 1));
			var firstDayOfCurrentWeek = this.SelectedDate.AddDays(-((int)this.SelectedDate.DayOfWeek - 1));

			var weekStartIndex = (int)dtf.FirstDayOfWeek; // 1
														  //var wekkCurentIndex = (int)firstDayOfMonth.DayOfWeek;  // 0
			var wekkCurentIndex = this.CalendarMode == CalendarModeType.Month ? (int)firstDayOfMonth.DayOfWeek : (int)firstDayOfCurrentWeek.DayOfWeek;  // 0

			DateTime inicioCalendario;

			if (wekkCurentIndex == weekStartIndex)
			{
				inicioCalendario = this.CalendarMode == CalendarModeType.Month ? firstDayOfMonth : firstDayOfCurrentWeek;
			}
			else if (wekkCurentIndex > weekStartIndex)
			{
				if (this.CalendarMode == CalendarModeType.Month)
					inicioCalendario = firstDayOfMonth.AddDays(-(wekkCurentIndex - weekStartIndex));
				else
					inicioCalendario = firstDayOfCurrentWeek.AddDays(-(wekkCurentIndex - weekStartIndex));
			}
			else
			{
				if (this.CalendarMode == CalendarModeType.Month)
					inicioCalendario = firstDayOfMonth.AddDays(-((wekkCurentIndex + 7) - weekStartIndex));
				else
					inicioCalendario = firstDayOfCurrentWeek.AddDays(-((wekkCurentIndex + 7) - weekStartIndex));
			}

			for (var i = 0; i < (int)this.CalendarMode; i++)
			{
				var diaData = DayData[i];
				var date = inicioCalendario.AddDays(i);
				diaData.Day = date;
				diaData.HasEvents = SpecialDays.Select(d => d.Date).Contains(date.Date);

				diaData.IsCurrentMonth = date.Month == firstDayOfMonth.Month;
			}
		}

		public void UpdateLayout()
		{
			
		}

		#region Events
		public event EventHandler<DateTime> OnDayClick;

		private Command clickDayCommand;

		public ICommand PrevMonthCommand { get { return new Command(Prev); } }
		public ICommand NextMonthCommand { get { return new Command(Next); } }

		void Prev()
		{
			try
			{
				if (this.CalendarMode == CalendarModeType.Month)
					SelectedDate = SelectedDate.AddMonths(-1);
				else if (this.CalendarMode == CalendarModeType.Week)
					SelectedDate = SelectedDate.AddDays(-7);
			}
			catch { }
		}
		void Next()
		{
			try
			{
				if (this.CalendarMode == CalendarModeType.Month)
					SelectedDate = SelectedDate.AddMonths(1);
				else if (this.CalendarMode == CalendarModeType.Week)
					SelectedDate = SelectedDate.AddDays(7);
			}
			catch { }
		}

		#endregion

		#region Props
		public static readonly BindableProperty PrevMonthImageSourceProperty = BindableProperty.Create<CalendarView, string>(
			p => p.PrevMonthImageSource, string.Empty, BindingMode.Default, null, PrevMonthImageSourcePropertyChanged);
		public string PrevMonthImageSource
		{
			get { return (string)GetValue(PrevMonthImageSourceProperty); }
			set { SetValue(PrevMonthImageSourceProperty, value); }
		}
		private static void PrevMonthImageSourcePropertyChanged(BindableObject bindable, string oldValue, string newValue)
		{
		}

		public static readonly BindableProperty PrevMonthImageScaleProperty = BindableProperty.Create<CalendarView, double>(
			p => p.PrevMonthImageScale, 1, BindingMode.Default, null, PrevMonthImageScalePropertyChanged);
		public double PrevMonthImageScale
		{
			get { return (double)GetValue(PrevMonthImageScaleProperty); }
			set { SetValue(PrevMonthImageScaleProperty, value); }
		}
		private static void PrevMonthImageScalePropertyChanged(BindableObject bindable, double oldValue, double newValue)
		{
		}

		public static readonly BindableProperty NextMonthImageSourceProperty = BindableProperty.Create<CalendarView, string>(
			p => p.NextMonthImageSource, string.Empty, BindingMode.Default, null, NextMonthImageSourcePropertyChanged);
		public string NextMonthImageSource
		{
			get { return (string)GetValue(NextMonthImageSourceProperty); }
			set { SetValue(NextMonthImageSourceProperty, value); }
		}
		private static void NextMonthImageSourcePropertyChanged(BindableObject bindable, string oldValue, string newValue)
		{
		}

		public static readonly BindableProperty NextMonthImageScaleProperty = BindableProperty.Create<CalendarView, double>(
			p => p.NextMonthImageScale, 1, BindingMode.Default, null, NextMonthImageScalePropertyChanged);
		public double NextMonthImageScale
		{
			get { return (double)GetValue(NextMonthImageScaleProperty); }
			set { SetValue(NextMonthImageScaleProperty, value); }
		}
		private static void NextMonthImageScalePropertyChanged(BindableObject bindable, double oldValue, double newValue)
		{
		}

		public static readonly BindableProperty MonthNameTextColorProperty = BindableProperty.Create<CalendarView, Color>(
			p => p.MonthNameTextColor, Color.Default, BindingMode.Default, null, MonthNameTextColorPropertyChanged);
		public Color MonthNameTextColor
		{
			get { return (Color)GetValue(MonthNameTextColorProperty); }
			set { SetValue(MonthNameTextColorProperty, value); }
		}
		private static void MonthNameTextColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
			(bindable as CalendarView).MesLabel.TextColor = newValue;
		}

		public static readonly BindableProperty CalendarModeProperty = BindableProperty.Create<CalendarView, CalendarModeType>(
			p => p.CalendarMode, CalendarModeType.Month, BindingMode.Default, null, CalendarModePropertyChanged);
		public CalendarModeType CalendarMode
		{
			get { return (CalendarModeType)GetValue(CalendarModeProperty); }
			set { SetValue(CalendarModeProperty, value); }
		}
		private static void CalendarModePropertyChanged(BindableObject bindable, CalendarModeType oldValue, CalendarModeType newValue)
		{
			if ((bindable as CalendarView).Content != null)
				(bindable as CalendarView).InitializeCalendar();
		}

		public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create<CalendarView, DateTime>(
			p => p.SelectedDate, DateTime.Now.Date, BindingMode.TwoWay, null, SelectedDatePropertyChanged);
		public DateTime SelectedDate
		{
			get { return (DateTime)GetValue(SelectedDateProperty); }
			set { SetValue(SelectedDateProperty, value); }
		}
		private static void SelectedDatePropertyChanged(BindableObject bindable, DateTime oldValue, DateTime newValue)
		{
			(bindable as CalendarView).UpdateDayData();
		}

		public static readonly BindableProperty CurrentDateHilightProperty = BindableProperty.Create<CalendarView, CurrentDayHilightType>(
			p => p.CurrentDateHilight, CurrentDayHilightType.CircularBackground, BindingMode.Default, null, CurrentDateHilightPropertyChanged);
		public CurrentDayHilightType CurrentDateHilight
		{
			get { return (CurrentDayHilightType)GetValue(CurrentDateHilightProperty); }
			set { SetValue(CurrentDateHilightProperty, value); }
		}
		private static void CurrentDateHilightPropertyChanged(BindableObject bindable, CurrentDayHilightType oldValue, CurrentDayHilightType newValue)
		{
		}

		public static readonly BindableProperty CurrentDateHilightBackgroundColorProperty = BindableProperty.Create<CalendarView, Color>(
			p => p.CurrentDateHilightBackgroundColor, Color.Gray, BindingMode.Default, null, CurrentDateHilightBackgroundColorPropertyChanged);
		public Color CurrentDateHilightBackgroundColor
		{
			get { return (Color)GetValue(CurrentDateHilightBackgroundColorProperty); }
			set { SetValue(CurrentDateHilightBackgroundColorProperty, value); }
		}
		private static void CurrentDateHilightBackgroundColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
		}

		public static readonly BindableProperty EventDayMarkerProperty = BindableProperty.Create<CalendarView, EventDayMarkerType>(
			p => p.EventDayMarker, EventDayMarkerType.Dot, BindingMode.Default, null, EventDayMarkerPropertyChanged);
		public EventDayMarkerType EventDayMarker
		{
			get { return (EventDayMarkerType)GetValue(EventDayMarkerProperty); }
			set { SetValue(EventDayMarkerProperty, value); }
		}
		private static void EventDayMarkerPropertyChanged(BindableObject bindable, EventDayMarkerType oldValue, EventDayMarkerType newValue)
		{
		}

		public static readonly BindableProperty EventDayMarkerColorProperty = BindableProperty.Create<CalendarView, Color>(
			p => p.EventDayMarkerColor, Color.Default, BindingMode.Default, null, EventDayMarkerColorPropertyChanged);
		public Color EventDayMarkerColor
		{
			get { return (Color)GetValue(EventDayMarkerColorProperty); }
			set { SetValue(EventDayMarkerColorProperty, value); }
		}
		private static void EventDayMarkerColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
		}

		public static readonly BindableProperty DayColorProperty = BindableProperty.Create<CalendarView, Color>(
			p => p.DayColor, Color.Default, BindingMode.Default, null, DayColorPropertyChanged);
		public Color DayColor
		{
			get { return (Color)GetValue(DayColorProperty); }
			set { SetValue(DayColorProperty, value); }
		}
		private static void DayColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
		}

		public static readonly BindableProperty WeekDayColorProperty = BindableProperty.Create<CalendarView, Color>(
			p => p.WeekDayColor, Color.Default, BindingMode.Default, null, WeekDayColorPropertyChanged);
		public Color WeekDayColor
		{
			get { return (Color)GetValue(WeekDayColorProperty); }
			set { SetValue(WeekDayColorProperty, value); }
		}
		private static void WeekDayColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
		}

		public static readonly BindableProperty OtherMonthDayColorProperty = BindableProperty.Create<CalendarView, Color>(
			p => p.OtherMonthDayColor, Color.Default, BindingMode.Default, null, OtherMonthDayColorPropertyChanged);
		public Color OtherMonthDayColor
		{
			get { return (Color)GetValue(OtherMonthDayColorProperty); }
			set { SetValue(OtherMonthDayColorProperty, value); }
		}
		private static void OtherMonthDayColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
		{
		}


		public static readonly BindableProperty CalendarRowHeightProperty = BindableProperty.Create<CalendarView, double>(
			p => p.CalendarRowHeight, 40, BindingMode.Default, null, CalendarRowHeightPropertyChanged);
		public double CalendarRowHeight
		{
			get { return (double)GetValue(CalendarRowHeightProperty); }
			set { SetValue(CalendarRowHeightProperty, value); }
		}
		private static void CalendarRowHeightPropertyChanged(BindableObject bindable, double oldValue, double newValue)
		{
		}

		public static readonly BindableProperty SpecialDaysProperty = BindableProperty.Create<CalendarView, ObservableCollection<DateTime>>(
			p => p.SpecialDays, new ObservableCollection<DateTime>(), BindingMode.Default, null, SpecialDaysPropertyChanged);
		public ObservableCollection<DateTime> SpecialDays
		{
			get { return (ObservableCollection<DateTime>)GetValue(SpecialDaysProperty); }
			set { SetValue(SpecialDaysProperty, value); }
		}
		private static void SpecialDaysPropertyChanged(BindableObject bindable, ObservableCollection<DateTime> oldValue, ObservableCollection<DateTime> newValue)
		{
			(bindable as CalendarView).UpdateDayData();
		}

		public static readonly BindableProperty BrowsableProperty = BindableProperty.Create<CalendarView, bool>(
			p => p.Browsable, true, BindingMode.Default, null, BrowsablePropertyChanged);
		public bool Browsable
		{
			get { return (bool)GetValue(BrowsableProperty); }
			set { SetValue(BrowsableProperty, value); }
		}
		private static void BrowsablePropertyChanged(BindableObject bindable, bool oldValue, bool newValue)
		{
			(bindable as CalendarView).gridMes.IsVisible = newValue;
		}


		private ObservableCollection<CalendarViewDay> DayData
		{
			get;
			set;
		} = new ObservableCollection<CalendarViewDay>();
		#endregion
	}

	public class MonthNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime && targetType == typeof(string))
			{
				return ((DateTime)value).ToString((string)parameter).ToUpper();
			}
			return "N/a";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}

