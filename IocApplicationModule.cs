using System;
using Autofac;
using Prism.Events;

namespace SampleApplication
{
	public class IocApplicationModule : Module
	{
		protected override void Load (ContainerBuilder builder)
		{
			base.Load (builder);

			builder.RegisterType<EventAggregator>().As<IEventAggregator>().AsSelf ().SingleInstance();
			builder.RegisterType<Repository>().As<IRepository>().AsSelf().SingleInstance();
			builder.RegisterType(typeof(XFormsNavigationService)).As(typeof(INavigationService)).AsSelf().SingleInstance();

			builder.RegisterType (typeof(XFormsUserNotifier)).As (typeof(IUserNotifier)).AsSelf();

			builder.RegisterType<SampleItemValidator>().As<IModelValidator<SampleItem>>().AsSelf();

			builder.RegisterType<MainViewModel>().Keyed<IViewModel>(Constants.Navigation.MainPage);
			builder.RegisterType<ItemViewModel>().Keyed<IViewModel>(Constants.Navigation.ItemPage);

			builder.RegisterType<MainPage>().Keyed<IView>(Constants.Navigation.MainPage);
			builder.RegisterType(typeof(ItemPage)).Keyed(Constants.Navigation.ItemPage, typeof(IView));

		}
	}
}

