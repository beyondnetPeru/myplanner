
using MyPlanner.IntegrationEventLogEF.Services;
using MyPlanner.Shared.Infrastructure.Idempotency;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Infrastructure.Repositories;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Infrastructure.Idempotency;
using MyPlanner.Plannings.Api.UseCases.SizeModels.Queries;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries;
using MyPlanner.Plannings.Api.UseCases.Plan.Queries;
using MyPlanner.Plannings.Api.Behaviors;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer;
using MyPlanner.Plannings.Api.Boostrapper;
using MyPlanner.Shared.Mediator.Behaviors;

namespace MyPlanner.Plannings.Api.Extensions
{
    public static partial class ApiServicesBuilderExtensions
    {
        /// <summary>
        /// Adds application services to the host application builder.
        /// </summary>
        /// <param name="builder">The host application builder.</param>
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PlanningDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var assembly = typeof(Program).Assembly;

            builder.Services.AddCarter();
            
            builder.Services.AddAutoMapper(assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            builder.Services.AddFactory(c =>
            {
                c.AddSource<SizeModelTypeFactorCostConfigurationSource>();
                c.AddTransient<ISizeModelTypeFactorCostFactory, SizeModelTypeFactorCostFactory>();
                c.AddTransient<ISizeModelTypeFactorCostCalculator, SizeModelTypeTShirtAndSprintFactorCostCalculator>();
                c.AddTransient<ISizeModelTypeFactorCostCalculator, SizeModelTypeDefaultFactorDefaultCostCalculator>();
            });

            builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<PlanningDbContext>>();
            builder.Services.AddTransient<IPlanningIntegrationEventService, PlanningIntegrationEventService>(); 

            builder.Services.AddScoped<ISizeModelTypeRepository, SizeModelTypeRepository>();
            builder.Services.AddScoped<ISizeModelTypeQueryRepository, SizeModelTypeQueryRepository>();
            builder.Services.AddScoped<ISizeModelRepository, SizeModelRepository>();
            builder.Services.AddScoped<ISizeModelQueryRepository, SizeModelQueryRepository>();
            builder.Services.AddTransient<IPlanRepository, PlanRepository>();
            builder.Services.AddTransient<IPlanQueryRepository, PlanQueryRepository>();
            
            builder.Services.AddScoped<IRequestManager, RequestManager>();


            //builder.AddRabbitMqEventBus("eventbus")
            //       .AddEventBusSubscriptions();
        }

        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            //eventBus.AddSubscription<IntegrationEvent, IntegrationEventHandler>();
        }
    }
}
