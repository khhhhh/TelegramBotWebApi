using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    public class LohCommand : ICommandBase
    {
        private readonly ITelegramBotClient Bot;
        private readonly DataContext _context;

        public LohCommand(ITelegramBotClient bot, DataContext context)
        {
            Bot = bot;
            _context = context;
        }

        public string Name => "loh";

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                long chatID = update.Message.Chat.Id;

                var chat = _context
                    .Chats
                    .Include(chat => chat.Users)
                    .First(chat => chat.TelegramId == chatID);

                var users = chat
                    .Users;

                int randomNumber = new Random((int)DateTime.Now.Date.Ticks).Next(users.Count);
                string text = $"@{users[randomNumber].UserName}, поздравляю! Ты лох дня!";

                if (chat.PinnedMessage.Equals(text))
                {
                    if (chat.MessageId != 0)
                    {
                        Message message = await Bot.SendTextMessageAsync(
                            chatId: chatID,
                            text: ".",
                            replyToMessageId: chat.MessageId
                            );
                    }
                    return;
                }

                chat.PinnedMessage = text;

                Message reply = await Bot.SendTextMessageAsync(
                    chatId: chatID,
                    text: text
                    );

                chat.MessageId = reply.MessageId;
                await _context.SaveChangesAsync();

            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
