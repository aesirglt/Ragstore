﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Accounts.QueriesHandler;
using FunctionalConcepts.Results;

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Accounts.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.Accounts;

public class AccountByIdHandler : IRequestHandler<AccountByIdQuery, Result<IQueryable<Account>>>
{
    public Task<Result<IQueryable<Account>>> Handle(AccountByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
