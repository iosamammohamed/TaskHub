namespace TaskHub.Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; } // Nullable only if not authenticated
    string? ADUserId { get; }
    string? Email { get; }
    string? Name { get; }
    bool IsAuthenticated { get; }

    // Internal method for middleware use only
    void SetUser(int userId);
}



