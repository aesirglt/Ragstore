namespace Totten.Solution.Ragstore.ApplicationService.Services;

using Autofac;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.DTOs.Messages;
using Totten.Solution.Ragstore.ApplicationService.Interfaces;

public class WhatsAPPService(IComponentContext httpClientFactory)
    : IMessageService<NotificationMessageDto>
{
    private readonly HttpClient _httpClient = httpClientFactory.ResolveNamed<HttpClient>("UrlApiWPP");

    public async Task<Result<Success>> Send(NotificationMessageDto sendableClass)
    {
        try
        {
            var json = JsonConvert.SerializeObject(new
            {
                number = sendableClass.To,
                textMessage = new
                {
                    text = sendableClass.Content
                }
            });

            var httpMessage = new HttpRequestMessage(HttpMethod.Post, "message/sendText/ragnastore")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(httpMessage);

            return default(Success);
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro na api de envio de mensagem do whatsapp", ex);
            return error;
        }
    }
}
