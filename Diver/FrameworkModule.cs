using System.Windows;
using System.Windows.Controls;
using Autofac;
using Diver.Common;
using Diver.Pages;

namespace Diver.Infrastructure
{
    public class FrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<NavigationManager>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<MainWindow>()
                .AsSelf()
                .As<IContentPresenter>()
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsSubclassOf(typeof(Window)))
                .Except<MainWindow>()
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
                .OnActivated(s => (s.Instance as ViewModelBase).Activated())
                .OnRelease(s => (s as ViewModelBase).Dispose());
        }
    }
}
