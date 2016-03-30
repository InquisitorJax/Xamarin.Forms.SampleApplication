using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SampleApplication
{
	public partial class ItemPage : ContentPage, IView
	{
		public ItemPage ()
		{
			InitializeComponent ();
		}

		public IViewModel ViewModel { get; set; }
	}
}

