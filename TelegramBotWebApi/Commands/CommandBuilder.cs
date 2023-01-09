using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotWebApi.Models;
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

        public ICommandBase GetAnimeJoke()
        {
            return new AnimeCommand(bot);
        }

        public ICommandBase? GetCommandBase(string command)
        {

            if (command == null)
                return null;

            command = command.ToLower();

            if (command.StartsWith($"/{Consts.DICK_COMMAND}"))
                return new DickCommand(bot, dateService);
            else if (command.StartsWith($"/{Consts.POPA_COMMAND}"))
                return new PopaCommand(bot);
            else if (command.StartsWith($"/{Consts.SEX_COMMAND}"))
                return new SexCommand(bot, context);
            else if (command.StartsWith($"/{Consts.LOH_COMMAND}"))
                return new LohCommand(bot, context);
            else if (command.StartsWith($"/{Consts.TRUTH_COMMAND}"))
                return new TruthOrDareCommand(bot, "truth");
            else if (command.StartsWith($"/{Consts.DARE_COMMAND}"))
                return new TruthOrDareCommand(bot, "dare");
            else if (command.StartsWith($"/{Consts.BIRTHDAY_COMMAND}"))
                return new NextBDayCommand(bot, context);
            return null;
        }
    }
}
