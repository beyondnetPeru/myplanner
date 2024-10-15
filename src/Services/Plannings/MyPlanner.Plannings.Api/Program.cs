using MyPlanner.Plannings.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServicesDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.MapCarter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();