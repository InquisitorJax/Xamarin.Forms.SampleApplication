using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SampleApplication
{
	public partial class MainPage : ContentPage, IView
	{
		public MainPage ()
		{
			InitializeComponent ();

			Title = "Sample Xamarin Forms App";
		}

		#region IView implementation

		public IViewModel ViewModel { get; set; }

		#endregion
	}
}

