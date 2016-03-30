using System;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SampleApplication
{
	public class ViewModelBase : BindableBase, IViewModel
	{
		private bool _isBusy;
		private string _busyMessage;

		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value); }
		}

		public string BusyMessage
		{
			get { return _busyMessage; }
			set { SetProperty(ref _busyMessage, value); }
		}

		protected void ShowBusy(string message)
		{
			IsBusy = true;
			BusyMessage = message;
		}

		protected async Task Close()
		{
			await Navigation.GoBack();
		}

		protected void NotBusy()
		{
			IsBusy = false;
			BusyMessage = null;
		}

		protected IUserNotifier UserNotifier
		{
			get { return CC.UserNotifier; }
		}

		protected INavigationService Navigation
		{
			get { return CC.Navigation;}
		}

		#region IViewModel implementation

		public virtual Task InitializeAsync (Dictionary<string, string> args)
		{
			return Task.FromResult (default(int));
		}

		public virtual Task LoadStateAsync ()
		{
			return Task.FromResult (default(int));
		}

		public virtual Task SaveStateAsync ()
		{
			return Task.FromResult (default(int));
		}

		public virtual void Closing ()
		{
			
		}

		#endregion
	}
}

