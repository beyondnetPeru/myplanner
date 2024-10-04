namespace MyPlanner.Shared.Extensions
{
    public static partial class ApiDefaultsExtensions
    {
        /// <summary>
        /// Adds the default services for the application.
        /// </summary>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IHostApplicationBuilder"/> instance.</returns>
        public static IHostApplicationBuilder AddServicesDefaults(this IHostApplicationBuilder builder)
        {
            builder.AddBasicServiceDefaults();

            builder.Services.AddServiceDiscovery();

            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                // Turn on resilience by default
                http.AddStandardResilienceHandler();

                // Turn on service discovery by default
                http.AddServiceDiscovery();
            });

            return builder;
        }

        /// <summary>
        /// Adds the services except for making outgoing HTTP calls.
        /// </summary>
        /// <remarks>
        /// This allows for things like Polly to be trimmed out of the app if it isn't used.
        /// </remarks>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IHostApplicationBuilder"/> instance.</returns>
        public static IHostApplicationBuilder AddBasicServiceDefaults(this IHostApplicationBuilder builder)
        {
            // Default health checks assume the event bus and self health checks
            builder.AddDefaultHealthChecks();

            return builder;
        }

        /// <summary>
        /// Adds the default health checks for the application.
        /// </summary>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IHostApplicationBuilder"/> instance.</returns>
        public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                // Add a default liveness check to ensure app is responsive
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" });

            return builder;
        }

        /// <summary>
        /// Maps the default endpoints for the application.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder MapDefaultsEndpoints(this IApplicationBuilder app)
        {
            // Uncomment the following line to enable the Prometheus endpoint (requires the OpenTelemetry.Exporter.Prometheus.AspNetCore package)
            // app.MapPrometheusScrapingEndpoint();

            // Adding health checks endpoints to applications in non-development environments has security implications.
            // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
            if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                // All health checks must pass for app to be considered ready to accept traffic after starting
                app.UseHealthChecks("/health");

                // Only health checks tagged with the "live" tag must pass for app to be considered alive
                app.UseHealthChecks("/alive", new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("live")
                });
            }

            return app;
        }
    }
}
