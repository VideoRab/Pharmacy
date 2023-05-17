using Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Common.Services;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserIdentity()
    {
        var userIdentity = _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;
        if (userIdentity is null)
        {
            throw new NullReferenceException("User identity not found");
        }

        return Guid.Parse(userIdentity);
    }
}
