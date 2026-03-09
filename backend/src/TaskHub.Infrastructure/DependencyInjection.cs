using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Infrastructure.Identity;
using TaskHub.Infrastructure.Interceptors;
using TaskHub.Infrastructure.Persistence;
using TaskHub.Infrastructure.Repositories;
using TaskHub.Infrastructure.Services;

namespace TaskHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditingSaveChangesInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventsSaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IWorkItemRepository, WorkItemRepository>();
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();



        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration["Authentication:Authority"]; // ADFS/Auth0 URL
                options.Audience = configuration["Authentication:Audience"];   // Relying Party Identifier / API Identifier
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });


        return services;

    }
}
 
