using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Application.Common.Models;
using TaskHub.Application.Features.WorkItems.DTOs;

namespace TaskHub.Application.Features.WorkItems.Queries.ListWorkItems;

public class ListWorkItemsQueryHandler : IRequestHandler<ListWorkItemsQuery, PagedResult<WorkItemListItemDto>>
{
    private readonly IWorkItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public ListWorkItemsQueryHandler(IWorkItemRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<WorkItemListItemDto>> Handle(ListWorkItemsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new System.UnauthorizedAccessException();
        var result = await _repository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SearchTerm,
            request.Status,
            userId,
            cancellationToken);



        return new PagedResult<WorkItemListItemDto>
        {
            Items = result.Items.Select(x => x.Adapt<WorkItemListItemDto>()).ToList(),
            PageNumber = result.PageNumber,
            TotalPages = result.TotalPages,
            TotalCount = result.TotalCount
        };
    }
}
 
