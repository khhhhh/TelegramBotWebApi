using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    public interface ICommandBase
    {
        Task ExecuteAsync(Update update);
    }
}
