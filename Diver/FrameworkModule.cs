using System.Windows;
using System.Windows.Controls;
using Autofac;
using Diver.Common;

namespace Diver.Infrastructure
{
    public class FrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsSubclassOf(typeof(Window)))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsSubclassOf(typeof(Page)))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsSubclassOf(typeof(UserControl)))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsSubclassOf(typeof(ViewModelBase)))
                .AsSelf()
                .InstancePerLifetimeScope()
                .OnActivated(s => (s.Instance as ViewModelBase)?.Activated());
        }
    }
}
