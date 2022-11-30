using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    public class MockCommand : ICommandBase
    {
        public const string NAME = "dick";
        private readonly ITelegramBotClient Bot;
        public MockCommand(ITelegramBotClient bot)
        {
            Bot = bot;
        }

        public string Name => "MOCK";

        public async Task ExecuteAsync(Update update)
        {
            return;
        }
    }
}
