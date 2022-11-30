using Telegram.Bot;
using TelegramBotWebApi.Services;

namespace TelegramBotWebApi.Commands
{
    public class CommandBuilder
    {
        private readonly ITelegramBotClient bot;
        private readonly DataContext context;
        private readonly DateService dateService;

        public CommandBuilder(ITelegramBotClient bot, DataContext context, DateService service)
        {
            this.bot = bot;
            this.context = context;
            dateService = service;
        }

        public ICommandBase? GetCommandBase(string command)
        {

            if (command == null)
                return null;

            command = command.ToLower();

            if (command.StartsWith("/dick"))
                return new DickCommand(bot, dateService);
            else if (command.StartsWith("/popa"))
                return new PopaCommand(bot);
            else if (command.StartsWith("/sex"))
                return new SexCommand(bot, context);
            else if (command.StartsWith("/loh"))
                return new LohCommand(bot, context);
            else if (command.StartsWith("/truth"))
                return new TruthOrDareCommand(bot, "truth");
            else if (command.StartsWith("/dare"))
                return new TruthOrDareCommand(bot, "dare");
            else if (command.StartsWith("/nextbday"))
                return new NextBDayCommand(bot, context);
            return null;
        }
    }
}
