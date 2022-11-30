using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
                new BotCommand(){ Command="dick", Description="Размер хуя (100% правда)"},
                new BotCommand(){ Command="popa", Description="Пися?"},
                new BotCommand(){ Command="sex", Description="Узнай, с кем у тебя будет секс!"},
                new BotCommand(){ Command="loh", Description="Лох дня"},
                new BotCommand(){ Command="truth", Description="Правда"},
                new BotCommand(){ Command="dare", Description="Действие"},
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
