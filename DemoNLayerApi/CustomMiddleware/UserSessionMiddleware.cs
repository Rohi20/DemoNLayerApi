using DemoNLayerApi.Models.Models;
using System.Security.Claims;

namespace DemoNLayerApi.CustomMiddleware
{
    public class UserSessionMiddleware
    {
        private readonly RequestDelegate _next;
        public UserSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserSession session)
        {
            var contextUser = context?.User;
            if (contextUser != null && contextUser.Identities.Any())
            {
                session.Id = Convert.ToInt32(contextUser.FindFirstValue(ClaimTypes.NameIdentifier));
                session.Email = contextUser.FindFirstValue(ClaimTypes.Email);
                session.Name = contextUser.FindFirstValue(ClaimTypes.Name);
                session.Role = contextUser.FindFirstValue(ClaimTypes.Role);
            }      
                    
            await _next(context);
        }
    }
}
