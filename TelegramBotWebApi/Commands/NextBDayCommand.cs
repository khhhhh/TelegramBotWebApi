using Microsoft.EntityFrameworkCore;
using System.Globalization;
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
            var words = update.Message.Text.Split(' ');
            var answer = "";
            CultureInfo ci = new CultureInfo("ru-RU");
            if (words.Length > 1)
            {
                var userName = words.Last().Replace("@", "");
                var birthday = context
                    .Birthdays
                    .Include(b => b.User)
                    .FirstOrDefault(b => b.User != null && b.User.UserName == userName);

                if (birthday is null)
                    return;

                answer = $"День рождения @{birthday.User.UserName} - {birthday.Date.ToString("M", ci)}";
            }
            else
            {
                var currentDate = DateTime.Now;
                currentDate = DateTime.Parse($"2022-{currentDate.Month}-{currentDate.Day}");


                var birthday = await context
                    .Birthdays
                    .Where(bday => bday.Date.DayOfYear >= currentDate.DayOfYear)
                    .Include(b => b.User)
                    .OrderBy(b => b.Date)
                    .FirstOrDefaultAsync() ??
                    await
                    context.Birthdays.OrderBy(x => x.Date).FirstOrDefaultAsync();

                if (birthday is null)
                    return;


                var name = birthday.User?.UserName ?? birthday.Name;
                if (currentDate.DayOfYear == birthday.Date.DayOfYear)
                    answer = $"С днем рождения, @{name}!";
                else
                    answer = $"Следующий день рождения у @{name} - {birthday.Date.ToString("M", ci)}";
            }


            Message reply = await Bot.SendTextMessageAsync(
                       chatId: update.Message.Chat.Id,
                       text: answer);
        }
    }
}