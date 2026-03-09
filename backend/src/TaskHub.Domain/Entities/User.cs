using System.Collections.Generic;
using TaskHub.Domain.Common;

namespace TaskHub.Domain.Entities;

public class User : Entity<int>
{
    public string AdId { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string DisplayName { get; private set; } = null!;

    public ICollection<WorkItem> WorkItems { get; private set; } = new List<WorkItem>();

    private User() { }

    public User(string adId, string email, string displayName)
    {
        AdId = adId;
        Email = email;
        DisplayName = displayName;
    }

    public void UpdateProfile(string email, string displayName)
    {
        Email = email;
        DisplayName = displayName;
    }
}
