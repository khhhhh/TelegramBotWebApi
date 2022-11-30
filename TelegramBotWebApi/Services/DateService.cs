namespace TelegramBotWebApi.Services
{
    public class DateService
    {
        public DateTime creationTime { get; }
        public DateTime Now => DateTime.Now;

        public DateService()
        {
            creationTime = DateTime.Now.ToUniversalTime();
        }
    }
}
