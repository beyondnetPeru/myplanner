namespace MyPlanner.Shared.Extensions
{
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// Gets the generic type name of the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get the generic type name for.</param>
        /// <returns>The generic type name.</returns>
        public static string GetGenericTypeName(this Type type)
        {
            string typeName;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        /// <summary>
        /// Gets the generic type name of the specified <see cref="object"/>.
        /// </summary>
        /// <param name="object">The <see cref="object"/> to get the generic type name for.</param>
        /// <returns>The generic type name.</returns>
        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
