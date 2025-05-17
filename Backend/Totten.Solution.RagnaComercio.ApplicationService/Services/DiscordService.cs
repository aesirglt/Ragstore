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
    private readonly string _token = "MTM3MzAzNDQ5MzAwMjM4NzY0OQ.Gco6G-.UQ6sn8zSt6TIEM9jc_tjpxnGSWIT28DqPaDCA4";

    public DiscordService(ILogger<DiscordService> logger, DiscordSocketClient client)
    {
        _logger = logger;
        _client = client;
        _client.Log += LogAsync;

        _client.LoginAsync(TokenType.Bot, _token).Wait();
        _client.StartAsync().Wait();
    }

    ~DiscordService()
    {
        if (_client.LoginState == LoginState.LoggedIn)
        {
            _client.StopAsync().Wait();
            _client.LogoutAsync().Wait();
        }
    }
    public async Task<Result<Success>> Send(DiscordMessageDto sendableClass)
    {
        try
        {
            var guild = _client.Guilds.FirstOrDefault();
            if (guild != null)
            {
                var users = await guild.GetUsersAsync().FlattenAsync();
                var user = users.FirstOrDefault(u => u.Username == sendableClass.UserName);
                if (user != null)
                {
                    var dmChannel = await user.CreateDMChannelAsync();
                    await dmChannel.SendMessageAsync(sendableClass.Content);
                    await dmChannel.CloseAsync();
                    return Result.Success;
                }
            }

            return NotFoundError.New($"User:'{sendableClass.UserName}' not found in discord");
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
