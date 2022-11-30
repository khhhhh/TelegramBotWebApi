using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotWebApi.Commands;

namespace TelegramBotWebApi.Services
{
    public class HandleUpdateService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<HandleUpdateService> _logger;
        private readonly CommandBuilder _builder;
        private readonly DateTime _bornTime;

        public HandleUpdateService(ITelegramBotClient botClient, ILogger<HandleUpdateService> logger, CommandBuilder commandBuilder )
        {
            _botClient = botClient;
            _logger = logger;
            _builder = commandBuilder;
            _bornTime = DateTime.Now.ToUniversalTime();
        }

        public async Task EchoAsync(Update update)
        {
            /*
            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                UpdateType.Message => await BotOnMessageReceived(update!),
            };
            */
            await BotOnMessageReceived(update!);
        }

        private async Task BotOnMessageReceived(Update update)
        {
            if (update.Message is not { } message)
                return;
            var dateOfMessage = message.Date.ToUniversalTime();
            if (dateOfMessage <= _bornTime)
                return;
            if (message?.From?.Id == 802803140L)
                return;
            // Only process text messages
            if (message?.Text is not { } messageText)
                return;

            var command = _builder.GetCommandBase(messageText);
            if (command != null)
                await command.ExecuteAsync(update);
            return;
        }
    }
}
