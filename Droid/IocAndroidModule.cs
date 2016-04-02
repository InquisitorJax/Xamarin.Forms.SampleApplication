using Autofac;
using SampleApplication;
using System;

namespace Application.Droid
{
    public class IocAndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AndroidDatabaseConnectionFactory>().As<IDatabaseConnectionFactory>().AsSelf();
        }
    }
}