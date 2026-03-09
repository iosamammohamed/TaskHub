namespace TaskHub.Domain.Common;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    int? CreatedBy { get; set; }
    DateTime? ModifiedAt { get; set; }
    int? ModifiedBy { get; set; }
}

