
using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.IntegrationEventLogEF.Services;
using MyPlanner.Plannings.Shared.Infrastructure.Idempotency;
using MyPlanner.Plannings.Shared.Application.Behaviors;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Infrastructure.Repositories;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.PlanAggregate;
using Carter;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.Extensions
{
    public static partial class ApiServicesBuilderExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PlanningDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var assembly = typeof(Program).Assembly;

            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

            builder.Services.AddCarter();

            builder.Services.AddAutoMapper(assembly);

            builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<PlanningDbContext>>();
            builder.Services.AddTransient<IPlanningIntegrationEventService, PlanningIntegrationEventService>();
            builder.Services.AddScoped<ISizeModelTypeRepository, SizeModelTypeRepository>();
            builder.Services.AddScoped<ISizeModelRepository, SizeModelRepository>();
            builder.Services.AddTransient<IPlanRepository, PlanRepository>();
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
