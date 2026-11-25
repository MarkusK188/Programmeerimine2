using System.Linq;
using System.Security.Claims;
using KooliProjekt.Application.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KooliProjekt.WebAPI
{
    public class WebUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.User?.Identity?.IsAuthenticated != true) return null;

            var claim = context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if(claim == null)
            {
                return null;
            }

            return int.Parse(claim.Value);
        }
    }
}
