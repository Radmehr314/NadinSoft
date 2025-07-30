using Autofac;
using CNadinSoft.Infrastructure.Config;
using NadinSoft.Application.Contract.Framework;
using NaidnSoft.Application.CommandHandler;
using NaidnSoft.Application.QueryHandler;


namespace NadinSoft.Infrastructure.Config;

public class AutofacModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(UserCommandHandler).Assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
            .InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(UserQueryHandler).Assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IQueryHandler<,>))))
            .InstancePerLifetimeScope();
        builder.RegisterType<AutofacCommandBus>().As<ICommandBus>().InstancePerLifetimeScope();
        builder.RegisterType<AutofacQueryBus>().As<IQueryBus>().InstancePerLifetimeScope();

    }
}