using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotWebApi.Commands
{
    public class TruthOrDareCommand : ICommandBase
    {

        private readonly ITelegramBotClient Bot;
        private readonly string filename;
        public TruthOrDareCommand(ITelegramBotClient bot, string fileName)
        {
            Bot = bot;
            filename = fileName;
        }

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                long chatID = update.Message.Chat.Id;

                var allLines = await System.IO.File.ReadAllLinesAsync($"{filename}.txt");
                var random = new Random();
                var lineNumber = random.Next(0, allLines.Length);
                string text = allLines[lineNumber];

                Message reply = await Bot.SendTextMessageAsync(
                    chatId: chatID,
                    text: text,
                    replyToMessageId: update.Message.MessageId);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
