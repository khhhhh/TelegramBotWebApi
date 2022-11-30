using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotWebApi.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [Column(TypeName = "bigint")]
        public long TelegramId { get; set; }
        public string Name { get; set; } = string.Empty;

        public IList<User> Users { get; set; }

        public string PinnedMessage { get; set; } = string.Empty;

        public int MessageId { get; set; }
    }
}
