using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebApi.Commands
{
    internal class NextBDayCommand : ICommandBase
    {
        private readonly ITelegramBotClient Bot;
        private readonly DataContext context;

        public NextBDayCommand(ITelegramBotClient bot, DataContext context)
        {
            Bot = bot;
            this.context = context;
        }

        public async Task ExecuteAsync(Update update)
        {
            var userName = update.Message.Text.Split(' ').Last();
            var answer = "";
            if(!string.IsNullOrWhiteSpace(userName))
            {
                userName = userName.Replace("@", "");
                var birthday = context
                    .Birthdays
                    .Include(b => b.User)
                    .FirstOrDefault(b => b.User != null && b.User.UserName == userName);

                if (birthday is null)
                    return;

                answer = $"День рождения @{birthday.User.UserName} - {birthday.Date:M}";
            }    
            else
            {
                var birthday = context
                    .Birthdays
                    .Include(b => b.User)
                    .OrderBy(b => b.Date)
                    .FirstOrDefault();

                if (birthday is null)
                    return;

                answer = $"Следующий день рождения у @{birthday.User.UserName} - {birthday.Date:M}";
            }


            Message reply = await Bot.SendTextMessageAsync(
                       chatId: update.Message.Chat.Id,
                       text: answer);
        }
    }
}