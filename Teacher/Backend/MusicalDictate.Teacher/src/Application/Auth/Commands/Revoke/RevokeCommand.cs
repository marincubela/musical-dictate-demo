using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands.Revoke;

public class RevokeCommand : IRequest
{
    public RevokeCommand(string id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public string Id { get; }
}

public class RevokeCommandHandler : IRequestHandler<RevokeCommand>
{
    private readonly IIdentityService _identityService;

    public RevokeCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Unit> Handle(RevokeCommand request, CancellationToken cancellationToken)
    {
        await _identityService.Revoke(request.Id);
        return Unit.Value;
    }
}