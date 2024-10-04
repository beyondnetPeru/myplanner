namespace MyPlanner.Shared.Extensions;

public static class HttpClientExtensions
{
    /// <summary>
    /// Adds authentication token to the HttpClientBuilder.
    /// </summary>
    /// <param name="builder">The HttpClientBuilder.</param>
    /// <returns>The HttpClientBuilder with authentication token added.</returns>
    public static IHttpClientBuilder AddAuthToken(this IHttpClientBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.TryAddTransient<HttpClientAuthorizationDelegatingHandler>();

        builder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        return builder;
    }

    private class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientAuthorizationDelegatingHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The IHttpContextAccessor.</param>
        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientAuthorizationDelegatingHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The IHttpContextAccessor.</param>
        /// <param name="innerHandler">The inner HttpMessageHandler.</param>
        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Sends an HTTP request with an authentication token.
        /// </summary>
        /// <param name="request">The HttpRequestMessage.</param>
        /// <param name="cancellationToken">The CancellationToken.</param>
        /// <returns>The Task representing the asynchronous operation.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is HttpContext context)
            {
                var accessToken = await context.GetTokenAsync("access_token");

                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
