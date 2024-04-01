using Autofac;
using TodoList.Core.Interfaces;
using TodoList.Core.Services;

namespace TodoList.Core;

/// <summary>
///     An Autofac module that is responsible for wiring up services defined in the Core project.
/// </summary>
public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DeleteContributorService>()
            .As<IDeleteContributorService>().InstancePerLifetimeScope();
    }
}
