using System;

using Xamarin.Forms;
using Autofac;
using System.Collections.Generic;

namespace SampleApplication
{
	public class App : Xamarin.Forms.Application
	{
		public App (Module platformModule)
		{
			//TODO: Incorporate Splash screen to await initialization, and then navigation to main page
			Initialize (platformModule);
			// The root page of your application
			Navigation.NavigateAsync(Constants.Navigation.MainPage);
		}

		private INavigationService Navigation
		{
			get { return CC.IoC.Resolve<INavigationService>(); }
		}

		private void Initialize(Module platformModule)
		{

			List<Module> modules = new List<Module>
			{
				new IocApplicationModule(),
				platformModule
			};

			CC.InitializeIoc (modules.ToArray());

			var repository = CC.IoC.Resolve<IRepository>();
			repository.Initialize();
		}


		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override async void OnSleep ()
		{
			// Handle when your app sleeps
			await Navigation.SuspendAsync();
		}

		protected override async void OnResume ()
		{
			// Handle when your app resumes
			await Navigation.ResumeAsync();
		}
	}
}

