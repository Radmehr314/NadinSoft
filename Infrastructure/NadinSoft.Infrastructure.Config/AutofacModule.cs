using Autofac;
using CNadinSoft.Infrastructure.Config;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Domain;
using NadinSoft.Infrastructure.Persistance.SQl;
using NadinSoft.Infrastructure.Persistance.SQl.Repositories;
using NadinSoft.Application.CommandHandler;
using NadinSoft.Application.Contract.Contracts;
using NadinSoft.Application.QueryHandler;
using NadinSoft.Infrastructure.Persistance.SQl.Services;


namespace NadinSoft.Infrastructure.Config;

public class AutofacModule:Module
{
    private readonly string _connectionString;

    public AutofacModule(string connectionString)
    {
        _connectionString = connectionString;
    }
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
        
        // Register repositories without connectionString parameter since they use DataBaseContext
        builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
            
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();    
        builder.RegisterType<UserInfoService>().As<IUserInfoService>().InstancePerLifetimeScope();    


        builder.RegisterType<AutofacCommandBus>().As<ICommandBus>().InstancePerLifetimeScope();
        builder.RegisterType<AutofacQueryBus>().As<IQueryBus>().InstancePerLifetimeScope();


    }
}