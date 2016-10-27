using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class Login : ContentPage
	{
		public Login()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}


		async void SignIn_Clicked(object sender, System.EventArgs e)
		{
			await this.Navigation.PushAsync(new SignIn());
		}
	}
}

