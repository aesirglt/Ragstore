namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Notifications;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class CallbackMessageCommand : IRequest<Result<Success>>;