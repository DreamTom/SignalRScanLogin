var builder = WebApplication.CreateBuilder(args);

var container = builder.Services;

container.AddControllers();

container.AddSignalR();

container.Configure<SignConfig>(builder.Configuration.GetSection(nameof(SignConfig)));

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
