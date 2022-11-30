using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotWebApi.Models;

namespace TelegramBotWebApi.Services
{
    public class ConfigureWebhook : IHostedService
    {

        private readonly IServiceProvider _services;
        private readonly BotConfiguration _botConfig;
        public ConfigureWebhook(IServiceProvider serviceProvider,
                                   IConfiguration configuration)
        {
            _services = serviceProvider;
            _botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            await botClient.SetMyCommandsAsync(new List<BotCommand>()
            {
                new BotCommand(){ Command=Consts.DICK_COMMAND, Description="Размер хуя (100% правда)"},
                new BotCommand(){ Command=Consts.POPA_COMMAND, Description="Пися?"},
                new BotCommand(){ Command=Consts.SEX_COMMAND, Description="Узнай, с кем у тебя будет секс!"},
                new BotCommand(){ Command=Consts.LOH_COMMAND, Description="Лох дня"},
                new BotCommand(){ Command=Consts.TRUTH_COMMAND, Description="Правда"},
                new BotCommand(){ Command=Consts.DARE_COMMAND, Description="Действие"},
                new BotCommand(){ Command=Consts.BIRTHDAY_COMMAND, Description="Узнать следующий день рождения! (Можно дописать ник в телеграмме)"},
            });

            var webhookAddress = @$"{_botConfig.HostAddress}/api/bot";

            await botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: new UpdateType[] { UpdateType.Message, UpdateType.InlineQuery },
                cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
