namespace TaskHub.Application.Common.Models;

public abstract class PagedInput
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
