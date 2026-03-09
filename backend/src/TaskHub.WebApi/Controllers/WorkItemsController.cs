using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.Features.WorkItems.Commands.ChangeWorkItemStatus;
using TaskHub.Application.Features.WorkItems.Commands.CreateWorkItem;
using TaskHub.Application.Features.WorkItems.Commands.DeleteWorkItem;
using TaskHub.Application.Features.WorkItems.Commands.UpdateWorkItem;
using TaskHub.Application.Features.WorkItems.Queries.GetWorkItemById;
using TaskHub.Application.Features.WorkItems.Queries.ListWorkItems;
using TaskHub.Application.Features.WorkItems.DTOs;
using TaskHub.Application.Common.Models;
using TaskHub.Domain.Enums;

using Microsoft.AspNetCore.Authorization;

namespace TaskHub.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/work-items")]

public class WorkItemsController : ControllerBase
{
    private readonly ISender _sender;

    public WorkItemsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateWorkItemCommand command)
    {
        return await _sender.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkItemDto>> GetById(int id)
    {
        return Ok(await _sender.Send(new GetWorkItemByIdQuery { Id = id }));
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<WorkItemListItemDto>>> GetList([FromQuery] ListWorkItemsQuery query)
    {
        return Ok(await _sender.Send(query));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkItemCommand command)
    {
        command.Id = id;
        await _sender.Send(command);
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult> ChangeStatus([FromRoute] int id, [FromBody] ChangeWorkItemStatusCommand command)
    {
        command.Id = id;
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _sender.Send(new DeleteWorkItemCommand { Id = id });
        return NoContent();
    }
} 
