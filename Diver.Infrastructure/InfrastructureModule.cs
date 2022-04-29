using Autofac;
using Diver.Domain.Interfaces;

namespace Diver.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DockerFileBuilder>()
                .As<IDockerFileBuilder>()
                .InstancePerLifetimeScope();
        }
    }
}
