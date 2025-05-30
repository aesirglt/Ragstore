﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class ServerDeactiveCommand : IRequest<Result<Success>>
{
    public Guid ServerId { get; set; }
}
