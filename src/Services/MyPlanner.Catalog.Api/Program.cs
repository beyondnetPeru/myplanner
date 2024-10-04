using MyPlanner.Catalog.API.Data;
using MyPlanner.Shared.Exceptions.Handlers;
using MyPlanner.Shared.Mediator.Behaviors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

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
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions().InitializeWith<CatalogInitialData>();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(opt => {});

app.MapCarter();

//TODO: Merge use of HealthChecks and HealthChecksUI into Common DefaultServiceExtensions
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}); 

app.Run();
