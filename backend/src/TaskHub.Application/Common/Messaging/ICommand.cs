using MediatR;

namespace TaskHub.Application.Common.Messaging;

public interface ICommandMarker
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>, ICommandMarker
{
}

public interface ICommand : IRequest<Unit>, ICommandMarker
{
}
