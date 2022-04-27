using Autofac;
using Diver.Domain.Interfaces;

namespace Diver.Application
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FileStructureNavigationServiceFactory>()
                .As<IFileStructureNavigationServiceFactory>()
                .InstancePerLifetimeScope();
        }
    }
}
