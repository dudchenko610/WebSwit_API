using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebSwIT.PresentationLayer.Providers
{
    public class NameUserIdProvider : IUserIdProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NameUserIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual string GetUserId(HubConnectionContext connection)
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
