using Microsoft.EntityFrameworkCore;
using TelegramBotWebApi;
using TelegramBotWebApi.Commands;
using TelegramBotWebApi.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBotWebApi.Services;

var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;

var botConfig = _configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

builder.Services.AddHostedService<ConfigureWebhook>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<HandleUpdateService>();
builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient => 
        new TelegramBotClient(botConfig.BotToken, httpClient));

/*
builder.Services.AddHttpClient("memescreator", c =>
{
    c.BaseAddress = new Uri("https://api.memegen.link/");
});
*/

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(
        _configuration["ConnectionStrings:Db"]
        )
    );

builder.Services.AddScoped<CommandBuilder>();
builder.Services.AddSingleton<DateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();


app.MapControllers();

app.Run();
