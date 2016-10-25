using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class MonthPage : ContentPage
	{
		public MonthPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);

			TapGestureRecognizer tp = new TapGestureRecognizer();
			tp.Tapped += (sender, e) => Navigation.PushAsync(new Progress());
			favButton.GestureRecognizers.Add(tp);

			List<AgendaModel> list = new List<AgendaModel>();

			AgendaModel a1 = new AgendaModel();
			a1.Avatar = ImageSource.FromFile("Avatar1.png");
			a1.MarkerColor = (Color)App.Current.Resources["appColorOrange"];
			a1.Title = "New subpage for";
			a1.FriendName = "Janet";
			a1.Subtitle = "8 - 10am";
			a1.Location = "";

			AgendaModel a2 = new AgendaModel();
			a2.Avatar = ImageSource.FromFile("Avatar2.png");
			a2.MarkerColor = (Color)App.Current.Resources["appColorTeal"];
			a2.Title = "Catch up with";
			a2.FriendName = "Tom";
			a2.Subtitle = "11 - 12pm";
			a2.Location = "Hangouts";

			AgendaModel a3 = new AgendaModel();
			a3.Avatar = ImageSource.FromFile("Avatar3.png");
			a3.MarkerColor = (Color)App.Current.Resources["appColorPurple"];
			a3.Title = "Lunch with";
			a3.FriendName = "Diane";
			a3.Subtitle = "1pm";
			a3.Location = "Restaurant";

			list.Add(a1);
			list.Add(a2);
			list.Add(a3);

			agendaListView.ItemsSource = list;
		}
	}
}

