﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Users.QueriesHandler;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.Users;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class UserByEmailHandler(IUserRepository userRepository) : IRequestHandler<UserByEmailQuery, Result<User>>
{
    private readonly IUserRepository _repository = userRepository;
    public async Task<Result<User>> Handle(UserByEmailQuery request, CancellationToken cancellationToken)
    {
        var userDetail =
            _repository.GetAll(u => u.NormalizedEmail == request.NormalizedEmail)
            .FirstOrDefault();

        var resultUser = userDetail is null
            ? NotFoundError.New($"User with email: '{request.NormalizedEmail}' not found.")
            : Result.Of(userDetail);

        return await resultUser.AsTask();
    }
}
