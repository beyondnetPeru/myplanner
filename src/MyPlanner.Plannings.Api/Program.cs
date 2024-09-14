using MyPlanner.Plannings.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServicesDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.UseHttpsRedirection();

app.Run();