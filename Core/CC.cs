using System;
using Autofac;
using Prism.Events;

namespace SampleApplication
{
	/// <summary>
	/// Central Command
	/// </summary>
	public static class CC
	{
		static CC()
		{
		}

		public static void InitializeIoc(Module[] modules)
		{
			var builder = new ContainerBuilder();
			foreach (var module in modules)
			{
				builder.RegisterModule(module);
			}
			_container = builder.Build ();
		}


		private static IComponentContext _container;
		public static IComponentContext IoC
		{
			get {return _container; }
		}

		public static IUserNotifier UserNotifier
		{
			get { return _container.Resolve<IUserNotifier>();}
		}

		public static INavigationService Navigation
		{
			get { return _container.Resolve<INavigationService>();}
		}

		public static IEventAggregator EventMessenger
		{
			get { return _container.Resolve<IEventAggregator>();}
		}
	}
}

