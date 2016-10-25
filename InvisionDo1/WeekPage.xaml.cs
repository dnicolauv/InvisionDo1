using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InvisionDo1
{
	public class AgendaModel
	{
		public ImageSource Avatar { get; set; }
		public Color MarkerColor { get; set; }
		public string Title { get; set; }
		public string FriendName { get; set; }
		public string Subtitle { get; set; }
		public string Location { get; set; }
	}

	public partial class WeekPage : ContentPage
	{
		public WeekPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);


			TapGestureRecognizer tp = new TapGestureRecognizer();
			tp.Tapped += (sender, e) => Navigation.PushAsync(new MonthPage());
			favButton.GestureRecognizers.Add(tp);


			List<AgendaModel> list = new List<AgendaModel>();

			AgendaModel a1 = new AgendaModel();
			a1.Avatar = ImageSource.FromFile("Avatar1.png");
			a1.MarkerColor = Color.FromHex("#50D2C2");
			a1.Title = "New subpage from";
			a1.FriendName = "Jane";
			a1.Subtitle = "8 - 10am";
			a1.Location = "";

			AgendaModel a2 = new AgendaModel();
			a2.Avatar = ImageSource.FromFile("Avatar2.png");
			a2.MarkerColor = Color.Transparent;
			a2.Title = "Catch up with";
			a2.FriendName = "Tom";
			a2.Subtitle = "11 - 12pm";
			a2.Location = "Hangouts";

			AgendaModel a3 = new AgendaModel();
			a3.Avatar = ImageSource.FromFile("Avatar3.png");
			a3.MarkerColor = Color.Transparent;
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

