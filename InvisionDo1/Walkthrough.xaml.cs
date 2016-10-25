using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class Walkthrough : CarouselPage
	{
		public class WalkthroughModel
		{
			public int Index { get; set; }
			public string Name { get; set; }
			public string ImageUrl { get; set; }
			public string Text { get; set; }
			public string ButtonText { get; set; }
		}

		List<WalkthroughModel> list;

		public Walkthrough()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);

			var page1 = new WalkthroughModel();
			page1.Index = 0;
			page1.Name = "Page1";
			page1.Text = "Keep your work organized and quickly check your reminders with simple calendar";
			page1.ImageUrl = "Calendar.png";
			page1.ButtonText = "Next";

			var page2 = new WalkthroughModel();
			page2.Index = 1;
			page2.Name = "Page2";
			page2.Text = "Keep2 your work organized and quickly check your reminders with simple calendar";
			page2.ImageUrl = "Calendar.png";
			page2.ButtonText = "Next";

			var page3 = new WalkthroughModel();
			page3.Index = 2;
			page3.Name = "Page3";
			page3.Text = "Keep3 your work organized and quickly check your reminders with simple calendar";
			page3.ImageUrl = "Calendar.png";
			page3.ButtonText = "Continue";

			list = new List<WalkthroughModel>();

			list.Add(page1);
			list.Add(page2);
			list.Add(page3);

			ItemsSource = list;
		}

		protected override void OnCurrentPageChanged()
		{
			base.OnCurrentPageChanged();

			SetCurrentActiveIndicator();
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			if ((SelectedItem as WalkthroughModel).Index == 2)
			{
				try
				{
					Navigation.PushAsync(new WeekPage(), true);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else
			{
				int idx = (this.SelectedItem as WalkthroughModel).Index;

				idx++;
				if (idx > 2) idx = 0;

				Device.BeginInvokeOnMainThread(() =>
				{
					this.CurrentPage = Children.ElementAt<ContentPage>(idx);
				});
			}				
		}

		protected void SetCurrentActiveIndicator()
		{
			try
			{
				foreach (WalkthroughModel m in this.ItemsSource)
				{
					if (SelectedItem == m)
					{
						if (m != null)
						{
							int idx = this.list.IndexOf(m);

							int count = 0;
							foreach (View v in ((Grid)CurrentPage.Content).Children)
							{
								if (v.GetType() == typeof(StackLayout))
								{
									foreach (BoxView b in ((StackLayout)v).Children)
									{
										if (idx == count)
											b.BackgroundColor = (Color)App.Current.Resources["mainColor"];
										else
											b.BackgroundColor = Color.FromHex("#d2d2d4");

										count++;
									}
								}
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				
			}
		}
	}
}

