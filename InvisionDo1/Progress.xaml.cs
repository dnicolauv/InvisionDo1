using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InvisionDo1
{

	public class TaskCategoryModel
	{
		public Color CategoryColor { get; set; }
		public string CategoryName { get; set; }
		public int CategoryCount { get; set; }
	}

	public partial class Progress : ContentPage
	{
		public Progress()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);

			List<TaskCategoryModel> list = new List<TaskCategoryModel>();

			TaskCategoryModel a1 = new TaskCategoryModel();
			a1.CategoryColor = (Color)App.Current.Resources["appColorTeal"];
			a1.CategoryCount = 108;
			a1.CategoryName = "Completed";

			TaskCategoryModel a2 = new TaskCategoryModel();
			a2.CategoryColor = (Color)App.Current.Resources["appColorOrange"];
			a2.CategoryCount = 56;
			a2.CategoryName = "Snoozed";

			TaskCategoryModel a3 = new TaskCategoryModel();
			a3.CategoryColor = (Color)App.Current.Resources["appColorPurple"];
			a3.CategoryCount = 36;
			a3.CategoryName = "Overdue";

			list.Add(a1);
			list.Add(a2);
			list.Add(a3);

			taskCategoryStatusListView.ItemsSource = list;
		}
	}
}

