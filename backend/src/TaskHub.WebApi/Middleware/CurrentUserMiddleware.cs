using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Persistence;

namespace TaskHub.WebApi.Middleware;


public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService, ApplicationDbContext dbContext)
    {
        if (currentUserService.IsAuthenticated)
        {
            var adId = currentUserService.ADUserId;
            if (!string.IsNullOrEmpty(adId))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.AdId == adId);

                if (user == null)
                {
                    // Just-In-Time Provisioning
                    user = new User(adId, currentUserService.Email ?? "", currentUserService.Name ?? "New User");
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();
                }
                else if (user.Email != currentUserService.Email || user.DisplayName != currentUserService.Name)
                {
                    // Sync profile changes from identity provider
                    user.UpdateProfile(currentUserService.Email ?? user.Email, currentUserService.Name ?? user.DisplayName);
                    await dbContext.SaveChangesAsync();
                }

                currentUserService.SetUser(user.Id);
            }
        }

        await _next(context);
    }
}

