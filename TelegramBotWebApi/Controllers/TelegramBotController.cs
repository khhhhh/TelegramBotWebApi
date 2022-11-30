using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotWebApi.Commands;
using TelegramBotWebApi.Services;

namespace TelegramBotWebApi.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class TelegramBotController : ControllerBase
    {
        private readonly CommandBuilder _builder;
        private readonly DateService dateService;
        private readonly DataContext _context;
        private readonly MemService memService;

        public TelegramBotController(CommandBuilder builder,
                                     DateService dateService,
                                     DataContext context)
                                     //MemService memService)
        {
            _builder = builder;
            this.dateService = dateService;
            _context = context;
            //this.memService = memService;
        }

#if DEBUG
        [HttpGet("test")]
        public async Task<IEnumerable<string>> LOL()
        {
            var els = await memService.GetAllPics();
            return els;
        }
#endif

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            if (update.Message is not { } message)
                return NoContent();
            var dateOfMessage = message.Date.ToUniversalTime();
            if (dateOfMessage <= dateService.creationTime)
                return NoContent();
            if (message?.Text is not { } messageText)
                return NoContent();

            await SaveToDbAsync(update);

            try
            {
                var command = _builder.GetCommandBase(messageText);
                if (command != null)
                    await command.ExecuteAsync(update!);
            }
            catch { }
            return NoContent();
        }

        private async Task SaveToDbAsync(Update _update)
        {
            if (_update.Message is not { })
                return;
            long chatId = _update.Message.Chat.Id;
            long userId = _update.Message.From.Id;

            if (!_context.Chats.Any(chat => chat.TelegramId == chatId))
            {
                await createChat(
                     new Models.Chat()
                     {
                         Name = _update.Message.Chat.Title ?? "",
                         TelegramId = chatId
                     }
                );
            }

            Models.Chat chat = await _context.Chats.Include(c => c.Users).FirstAsync(x => x.TelegramId == chatId);

            if (!_context.Users.Any(user => user.TelegramId == userId))
            {
                Models.User user =
                     new Models.User()
                     {
                         Name = _update.Message.From.FirstName ?? "",
                         TelegramId = userId,
                         UserName = _update.Message.From.Username ?? "",
                     };
                chat.Users.Add(user);
            }
            else if (!chat.Users.Any(user => user.TelegramId == userId))
            {
                Models.User user = await _context.Users.FirstAsync(x => x.TelegramId == userId);

                chat.Users.Add(user);
            }

            await _context.SaveChangesAsync();
        }

        private async Task createUser(Models.User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        private async Task createChat(Models.Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
        }
    }
}
