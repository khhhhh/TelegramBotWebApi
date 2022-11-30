namespace TelegramBotWebApi.Models
{
    public class Birthday
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime Date { get; set; }

        public User? User { get; set; }

    }
}
