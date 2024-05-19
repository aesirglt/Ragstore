namespace Totten.Solution.Ragstore.ApplicationService.Services;
using Coravel.Invocable;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;

public class ScheduleCallbackService : IInvocable
{
    private ICallbackRepository _callbackRepository;

    public ScheduleCallbackService(ICallbackRepository callbackRepository)
    {
        _callbackRepository = callbackRepository;
    }
    public async Task Invoke()
    {
        await _callbackRepository.Save(null);
    }
}
