﻿using System;

using Xamarin.Forms;

namespace InvisionDo1
{
	public partial class App : Application
	{
		public App()
		{

			InitializeComponent();

			// The root page of your application
			MainPage = new NavigationPage(new Login());
			//MainPage = new MyPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

