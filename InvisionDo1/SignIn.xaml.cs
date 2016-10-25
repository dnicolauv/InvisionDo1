using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class SignIn : ContentPage
	{
		public SignIn()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			TapGestureRecognizer signInrec = new TapGestureRecognizer();
			signInrec.Tapped += signinButton_Clicked;
			signinButton.GestureRecognizers.Add(signInrec);
		}

		async void Join_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new Walkthrough());
		}

		async void signinButton_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new Login());
		}
	}
}

