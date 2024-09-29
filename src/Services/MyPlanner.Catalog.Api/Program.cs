using MyPlanner.Catalog.API.Data;
using MyPlanner.Shared.Exceptions.Handlers;
using MyPlanner.Shared.Mediator.Behaviors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddExceptionHandler<ApiCustomExceptionHandler>();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
}).UseLightweightSessions().InitializeWith<CatalogInitialData>();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseExceptionHandler(opt => {});

app.MapCarter();

app.Run();
