using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    public class SexCommand : ICommandBase
    {
        private readonly ITelegramBotClient Bot;
        private readonly DataContext _context;

        public SexCommand(ITelegramBotClient bot, DataContext context)
        {
            Bot = bot;
            _context = context;
        }
        public string Name => "sex";

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                long chatID = update.Message.Chat.Id;
                long userID = update.Message.From.Id;

                Models.Chat chat = _context
                    .Chats
                    .Include(chat => chat.Users)
                    .First(chat => chat.TelegramId == chatID);

                int listLen = chat.Users.Count;
                List<Models.User> clonnedList = chat.Users.ToList().GetRange(0, listLen);
                clonnedList.Remove(clonnedList.First(x => x.TelegramId == userID));

                if (clonnedList.Count == 0)
                    return;

                int randomNumber = new Random().Next(clonnedList.Count);

                Message reply = await Bot.SendTextMessageAsync(
                    chatId: chatID,
                    text: $"У тебя будет секс с {clonnedList[randomNumber].UserName} ({clonnedList[randomNumber].Name})",
                    replyToMessageId: update.Message.MessageId);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
