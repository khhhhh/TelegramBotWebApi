using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotWebApi.Services;

namespace TelegramBotWebApi.Commands
{
    public class DickCommand : ICommandBase
    {
        public const string NAME = "dick";
        private readonly ITelegramBotClient Bot;
        private readonly DateService dateService;
        public DickCommand(ITelegramBotClient bot, DateService dateService)
        {
            this.Bot = bot;
            this.dateService = dateService;
        }

        public string Name => NAME;
        public async Task ExecuteAsync(Update update)
        {
            
            Random random = new Random(unchecked((int)(update.Message.From.Id + dateService.Now.Date.Ticks)));
            int cockSize = random.Next() % 30;
            string answer = $"Размер хуя @{update.Message.From.Username} {cockSize} см.";
            Message reply = await Bot.SendTextMessageAsync(
                       chatId: update.Message.Chat.Id,
                       text: answer);
        }
    }
}
