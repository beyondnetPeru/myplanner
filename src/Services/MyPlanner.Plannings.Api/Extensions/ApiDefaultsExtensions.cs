namespace MyPlanner.Plannings.Api.Extensions
{
    public static partial class ApiDefaultsExtensions
    {
        /// <summary>
        /// Adds the default services and configurations for the API.
        /// </summary>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance.</param>
        /// <returns>The updated <see cref="IHostApplicationBuilder"/> instance.</returns>
        public static IHostApplicationBuilder AddServicesDefaults(this IHostApplicationBuilder builder)
        {
            // Enable Semantic Kernel OpenTelemetry
            //AppContext.SetSwitch("Microsoft.SemanticKernel.Experimental.GenAI.EnableOTelDiagnosticsSensitive", true);

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
        /// Adds the basic services and configurations for the API except for making outgoing HTTP calls.
        /// </summary>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance.</param>
        /// <returns>The updated <see cref="IHostApplicationBuilder"/> instance.</returns>
        /// <remarks>
        /// This allows for things like Polly to be trimmed out of the app if it isn't used.
        /// </remarks>
        public static IHostApplicationBuilder AddBasicServiceDefaults(this IHostApplicationBuilder builder)
        {
            // Default health checks assume the event bus and self health checks
            builder.AddDefaultHealthChecks();

            return builder;
        }

        /// <summary>
        /// Adds the default health checks for the API.
        /// </summary>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance.</param>
        /// <returns>The updated <see cref="IHostApplicationBuilder"/> instance.</returns>
        public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                // Add a default liveness check to ensure app is responsive
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

            return builder;
        }

        /// <summary>
        /// Maps the default endpoints for the API.
        /// </summary>
        /// <param name="app">The <see cref="WebApplication"/> instance.</param>
        /// <returns>The updated <see cref="WebApplication"/> instance.</returns>
        public static WebApplication MapDefaultsEndpoints(this WebApplication app)
        {
            // Uncomment the following line to enable the Prometheus endpoint (requires the OpenTelemetry.Exporter.Prometheus.AspNetCore package)
            // app.MapPrometheusScrapingEndpoint();

            // Adding health checks endpoints to applications in non-development environments has security implications.
            // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
            if (app.Environment.IsDevelopment())
            {
                // All health checks must pass for app to be considered ready to accept traffic after starting
                app.MapHealthChecks("/health");

                // Only health checks tagged with the "live" tag must pass for app to be considered alive
                app.MapHealthChecks("/alive", new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("live")
                });
            }

            return app;
        }
    }
}
