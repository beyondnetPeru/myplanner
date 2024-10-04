namespace MyPlanner.Shared.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the required value from the configuration.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="name">The name of the value.</param>
        /// <returns>The required value.</returns>
        public static string GetRequiredValue(this IConfiguration configuration, string name)
        {
            var value = configuration[name];

            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}");
            }

            return value;
        }
    }
}