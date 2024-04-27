using Totten.Solution.Ragstore.ApplicationService.Notifications.Callbacks;

namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Messages.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class MessageNotificationHandler : INotificationHandler<MessageNotification>
{
    private IMediator _mediator;
    private HttpClient _client;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <exception cref="Exception"></exception>
    public MessageNotificationHandler(IServiceProvider provider)
    {
        var scoped = provider.CreateScope();
        _mediator = scoped.ServiceProvider.GetService<IMediator>() ?? throw new Exception();
        var httpService = scoped.ServiceProvider.GetService<IHttpClientFactory>() ?? throw new Exception();
        _client = httpService.CreateClient("WhatsClientUrl");
    }

    public async Task Handle(MessageNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var postData = new StringContent(JsonConvert.SerializeObject(notification));
            HttpResponseMessage response = await _client.PostAsync("/", postData);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("A solicitação falhou com o código de status: " + response.StatusCode);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
