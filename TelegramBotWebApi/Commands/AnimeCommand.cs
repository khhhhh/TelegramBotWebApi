using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    public class AnimeCommand : ICommandBase
    {
        private readonly ITelegramBotClient Bot;
        public AnimeCommand(ITelegramBotClient bot)
        {
            Bot = bot;
        }

        public async Task ExecuteAsync(Update update)
        {
            long chatID = update.Message.Chat.Id;
            var allLines = await System.IO.File.ReadAllLinesAsync($"anime.txt");
            var lines = string.Join("\n", allLines);
            var jokes = lines.Split("//");
            var random = new Random();

            var lineNumber = random.Next(0, jokes.Length);
            string text = jokes[lineNumber];

            Message reply = await Bot.SendTextMessageAsync(
                chatId: chatID,
                text: text,
                replyToMessageId: update.Message.MessageId);
        }
    }
}
