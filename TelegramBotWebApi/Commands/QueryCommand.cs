using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace TelegramBotWebApi.Commands
{
    public class QueryCommand : ICommandBase
    {
        private readonly ITelegramBotClient bot;

        public string Name => "Query";

        public QueryCommand(ITelegramBotClient bot)
        {
            this.bot = bot;
        }

        public Task ExecuteAsync(Update update)
        {
            //var result = new InlineQueryResultPhoto();
            //await bot.AnswerInlineQueryAsync();
            return null;
        }
    }
}
