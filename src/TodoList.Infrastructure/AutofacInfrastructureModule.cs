using System.Reflection;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using TodoList.Core.ContributorAggregate;
using TodoList.Core.Interfaces;
using TodoList.Core.Services;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Data.Queries;
using TodoList.Infrastructure.Data.Queries.Board;
using TodoList.Infrastructure.Data.Queries.Board.Column;
using TodoList.Infrastructure.Data.Queries.Board.Column.Card;
using TodoList.Infrastructure.Data.Queries.Board.Column.Card.Comment;
using TodoList.Infrastructure.Data.Queries.Board.Column.Card.Details;
using TodoList.Infrastructure.Email;
using TodoList.UseCases.Boards.Column.Card.Comment.Create;
using TodoList.UseCases.Boards.Column.Card.Create;
using TodoList.UseCases.Boards.Column.Card.Details.GetByIdCard;
using TodoList.UseCases.Boards.Column.Card.Order;
using TodoList.UseCases.Boards.Column.Card.Patch;
using TodoList.UseCases.Boards.Column.Create;
using TodoList.UseCases.Boards.Column.Delete;
using TodoList.UseCases.Boards.Column.Order;
using TodoList.UseCases.Boards.Column.Patch;
using TodoList.UseCases.Boards.GetByName;
using TodoList.UseCases.Boards.List;
using TodoList.UseCases.Cards.List;
using TodoList.UseCases.Contributors.Create;
using TodoList.UseCases.Contributors.List;
using Module = Autofac.Module;

namespace TodoList.Infrastructure;

/// <summary>
///     An Autofac module responsible for wiring up services defined in Infrastructure.
///     Mainly responsible for setting up EF and MediatR, as well as other one-off services.
/// </summary>
public class AutofacInfrastructureModule : Module
{
    private readonly List<Assembly> _assemblies = new();
    private readonly bool _isDevelopment;

    public AutofacInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        _isDevelopment = isDevelopment;
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
        {
            _assemblies.Add(assembly);
        }
    }

    private void LoadAssemblies()
    {
        // TODO: Replace these types with any type in the appropriate assembly/project
        var coreAssembly = Assembly.GetAssembly(typeof(Contributor));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(AutofacInfrastructureModule));
        var useCasesAssembly = Assembly.GetAssembly(typeof(CreateContributorCommand));

        AddToAssembliesIfNotNull(coreAssembly);
        AddToAssembliesIfNotNull(infrastructureAssembly);
        AddToAssembliesIfNotNull(useCasesAssembly);
    }

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();
        if (_isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(builder);
        }
        else
        {
            RegisterProductionOnlyDependencies(builder);
        }

        RegisterEF(builder);
        RegisterQueries(builder);
        RegisterMediatR(builder);
        RegisterServices(builder);
    }

    private void RegisterEF(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .As(typeof(IReadRepositoryBase<>))
            .As(typeof(IRepositoryBase<>))
            .InstancePerLifetimeScope();
    }

    private void RegisterQueries(ContainerBuilder builder)
    {
        builder.RegisterType<ListContributorsQueryService>()
            .As<IListContributorsQueryService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ListCardsQueryService>()
            .As<IListCardsQueryService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ListBoardQueryService>()
            .As<IListBoardQueryService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<GetByNameBoardQueryService>()
            .As<IGetByNameBoardService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CreateColumnCommandService>()
            .As<ICreateColumnService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DeleteColumnCommandService>()
            .As<IDeleteColumnService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PatchColumnCommandService>()
            .As<IPatchColumnService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<OrderColumnCommandService>()
            .As<IOrderColumnService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CreateCardCommandService>()
            .As<ICreateCardService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PatchCardCommandService>()
            .As<IPatchCardService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<GetByIdCardQueryService>()
            .As<IGetByIdCardDetailsService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<OrderCardCommandService>()
            .As<IOrderCardService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CreateCommentCommandService>()
            .As<ICreateCommentService>()
            .InstancePerLifetimeScope();
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder
            .RegisterGeneric(typeof(LoggingBehavior<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();

        builder
            .RegisterType<MediatRDomainEventDispatcher>()
            .As<IDomainEventDispatcher>()
            .InstancePerLifetimeScope();

        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<,>), typeof(IRequestExceptionHandler<,,>), typeof(IRequestExceptionAction<,>),
            typeof(INotificationHandler<>)
        };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }
    }

    private void RegisterServices(ContainerBuilder builder)
    {
        var passwordSalt = Environment.GetEnvironmentVariable("PASSWORD_SALT_SECRET");
        if (string.IsNullOrEmpty(passwordSalt))
        {
            throw new ArgumentNullException(nameof(passwordSalt),
                "PASSWORD_SALT_SECRET was not found in environment variables");
        }

        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (string.IsNullOrEmpty(jwtSecret))
        {
            throw new ArgumentNullException(nameof(jwtSecret), "JWT_SECRET was not found in environment variables");
        }

        builder
            .RegisterType(typeof(PasswordService))
            .As(typeof(IPasswordService))
            .WithParameter("constantSalt", passwordSalt)
            .SingleInstance();

        builder
            .RegisterType(typeof(JwtService))
            .As(typeof(IJwtService))
            .WithParameter("jwtSecret", jwtSecret)
            .WithParameter("issuer", "https://todo.mishahub.com")
            .WithParameter("audience", "https://todo.mishahub.com/api")
            .SingleInstance();
    }

    private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
    {
        // NOTE: Add any development only services here
        builder.RegisterType<FakeEmailSender>().As<IEmailSender>()
            .InstancePerLifetimeScope();

        //builder.RegisterType<FakeListContributorsQueryService>()
        //  .As<IListContributorsQueryService>()
        //  .InstancePerLifetimeScope();
    }

    private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
    {
        // NOTE: Add any production only (real) services here
        builder.RegisterType<SmtpEmailSender>().As<IEmailSender>()
            .InstancePerLifetimeScope();
    }
}
