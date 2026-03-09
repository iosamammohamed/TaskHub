using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskHub.Application.Common.Interfaces;

namespace TaskHub.Infrastructure.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? ADUserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) 
                            ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");

    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)
                            ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("email");

    public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)
                                ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");

    public int? UserId { get; private set; }

    public void SetUser(int userId) => UserId = userId;

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;


}
