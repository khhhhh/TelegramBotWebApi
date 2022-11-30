using Microsoft.EntityFrameworkCore;
using TelegramBotWebApi.Models;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }

    public DbSet<Birthday> Birthdays { get; set; }

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Chat>().ToTable("Chats");
        modelBuilder.Entity<Birthday>().ToTable("Birthday");
    }
}