using Microsoft.AspNetCore.Http;
using MyPlanner.Shared.Services.IdentityServices.Interfaces;

namespace MyPlanner.Shared.Services.IdentityServices.Impl
{
    public class IdentityService(IHttpContextAccessor context) : IIdentityService
    {
        public string GetUserIdentity()
            => context.HttpContext?.User.FindFirst("sub")?.Value;

        public string GetUserName()
            => context.HttpContext?.User.Identity?.Name;
    }

}
