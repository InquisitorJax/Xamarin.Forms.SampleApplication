using System;
using Autofac;
using SampleApplication;

namespace Application.Droid
{
	public class IocAndroidModule : Module
	{
		protected override void Load (ContainerBuilder builder)
		{
			base.Load (builder);

			builder.RegisterType<AndroidDatabaseFactory>().As<IDatabaseConnectionFactory>().AsSelf();
		}
	}
}

