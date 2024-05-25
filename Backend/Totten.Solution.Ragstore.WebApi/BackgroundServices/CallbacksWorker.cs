namespace Totten.Solution.Ragstore.WebApi.BackgroundServices;

using Totten.Solution.Ragstore.ApplicationService.DTOs.Messages;
using Totten.Solution.Ragstore.ApplicationService.Interfaces;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

/// <summary>
/// 
/// </summary>
public class CallbacksWorker : BackgroundService
{
    private readonly ILogger<CallbacksWorker> _logger;
    private ICallbackScheduleRepository _repository;
    private IMessageService<NotificationMessageDto> _service;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="repository"></param>
    /// <param name="service"></param>
    public CallbacksWorker(ILogger<CallbacksWorker> logger,
        ICallbackScheduleRepository repository,
        IMessageService<NotificationMessageDto> service)
    {
        _logger = logger;
        _repository = repository;
        _service = service;
    }
    private async Task Invoke()
    {
        var callbacks = _repository.GetAll(x => !x.Sended && DateTime.Now >= x.SendIn).ToList();

        foreach (var cb in callbacks)
        {
            var response = await _service.Send(new NotificationMessageDto
            {
                To = cb.Contact,
                Content = cb.Body,
                From = "RagnaStore - Seu mercado de ragnarok online"
            });
            
            if (response.IsSuccess)
            {
                cb.Sended = true;
                await _repository.Update(cb);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Executando tarefa...");

                await Invoke();

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while class:{className} message: {errorMessage}", nameof(CallbacksWorker), ex.Message);
            }
        }
    }
}