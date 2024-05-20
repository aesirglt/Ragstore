namespace Totten.Solution.Ragstore.ApplicationService.Services;

using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.DTOs.Messages;
using Totten.Solution.Ragstore.ApplicationService.Interfaces;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class WhatsAPPService : IMessageService<NotificationMessageDto>
{
    private HttpClient _httpClient;

    public WhatsAPPService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("UrlApiWPP");
    }

    public async Task<Result<Exception, Unit>> Send(NotificationMessageDto sendableClass)
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

            var httpMessage = new HttpRequestMessage(HttpMethod.Post, "message/sendText/ragnastore");
            httpMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpMessage);

            return new Unit();
        }
        catch (Exception ex)
        {
            return Result<Exception, Unit>.Err(ex);
        }
    }
}
