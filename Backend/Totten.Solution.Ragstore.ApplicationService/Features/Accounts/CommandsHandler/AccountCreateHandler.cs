namespace Totten.Solution.Ragstore.ApplicationService.Features.Accounts.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Accounts.Commands;
using Totten.Solution.Ragstore.Domain.Features.Accounts;

public class AccountCreateHandler(
    IMapper mapper,
    IAccountRepository accountRepository) : IRequestHandler<AccountCreateCommand, Result<Success>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task<Result<Success>> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _mapper.Map<Account>(request);
            return await _accountRepository.Save(account);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
