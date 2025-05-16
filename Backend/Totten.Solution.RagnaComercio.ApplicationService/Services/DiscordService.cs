namespace Totten.Solution.RagnaComercio.ApplicationService.Services;

using Discord;
using Discord.WebSocket;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.DTOs.Messages;
using Totten.Solution.RagnaComercio.ApplicationService.Interfaces;

public class DiscordService : IMessageService<DiscordMessageDto>
{
    private ILogger<DiscordService> _logger;
    private DiscordSocketClient _client;
    private readonly string _token = "SEU_TOKEN_AQUI";

    public DiscordService(ILogger<DiscordService> logger, DiscordSocketClient client)
    {
        _logger = logger;
        _client = client;
        _client.Log += LogAsync;

        if (_client.LoginState != LoginState.LoggedIn)
        {
            Task.WaitAll(_client.LoginAsync(TokenType.Bot, _token));
            Task.WaitAll(_client.StartAsync());
        }
    }

    ~DiscordService()
    {
        if (_client.LoginState == LoginState.LoggedIn)
        {
            Task.WaitAll(_client.StopAsync());
            Task.WaitAll(_client.LogoutAsync());
        }
    }
    public async Task<Result<Success>> Send(DiscordMessageDto sendableClass)
    {
        try
        {
            if (_client.GetUser(sendableClass.UserName) is { } user)
            {
                var dmChannel = await user.CreateDMChannelAsync();
                await dmChannel.SendMessageAsync(sendableClass.Content);
                await dmChannel.CloseAsync();
            }
            return Result.Success;
        }
        catch (Exception ex)
        {
            UnhandledError err = ("Error to send message in discord", ex);
            return err;
        }
    }

    private Task LogAsync(LogMessage log)
    {
        if (log.Severity == LogSeverity.Error)
            _logger.LogError(log.ToString());

        if (log.Severity == LogSeverity.Critical)
            _logger.LogCritical(log.ToString());

        return Task.CompletedTask;
    }
}
