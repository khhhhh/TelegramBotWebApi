using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    public class PopaCommand : ICommandBase
    {
        private readonly ITelegramBotClient Bot;
        public PopaCommand(ITelegramBotClient bot)
        {
            Bot = bot;
        }
        private readonly string[] popa =
        {
            "Пися",
            "Попа",
            "Дядя",
            "Мама",
            "Папа",
            "Тётя",
            "Котя",
            "Мотя"
        };
        public const string NAME = "popa";
        public string Name => "/popa";

        public async Task ExecuteAsync(Update update)
        {
            var randomNum = new Random().Next(0, 100);
            char el = '\U00002728';
            var answer = "";
            if (randomNum == 99)
                answer =  $"{el} !Пися попа дядя тётя! {el}";
            else
            {
                var count = randomNum % popa.Length;
                answer = popa[count];
            }
            Message reply = await Bot.SendTextMessageAsync(
                       chatId: update.Message.Chat.Id,
                       text: answer);
        }
    }
}
