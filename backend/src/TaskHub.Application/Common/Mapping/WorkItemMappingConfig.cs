using Mapster;
using TaskHub.Application.Features.WorkItems.DTOs;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.Common.Mapping;

public class WorkItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WorkItem, WorkItemDto>();
        config.NewConfig<WorkItem, WorkItemListItemDto>();
    }
}
