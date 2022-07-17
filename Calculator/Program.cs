using Calculator.Services;
using Calculator.Services.Background;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ConfigService>();
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddSingleton<CalculateService>();
builder.Services.AddHostedService<MessageConsumer>();

WebApplication app = builder.Build();
app.Run();