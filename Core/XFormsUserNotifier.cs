using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Toasts;

namespace SampleApplication
{
	public class XFormsUserNotifier : IUserNotifier
	{
		#region IUserNotifier implementation

		public async Task ShowToastAsync(string message, string caption = "", int durationInSeconds = 2)
		{
			TimeSpan duration = TimeSpan.FromSeconds (durationInSeconds);
			var notificator = DependencyService.Get<IToastNotificator>();
			bool tapped = await notificator.Notify(ToastNotificationType.Info, caption, message, duration);
		}

		public async Task ShowMessageAsync(string message, string caption, string acceptButtonText = "Ok")
		{			
			Page currentPage = (Page)CC.Navigation.Current;
			await currentPage.DisplayAlert (caption, message, acceptButtonText);
		}

		#endregion
	}
}

