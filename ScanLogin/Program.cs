using ScanLogin;
using ScanLogin.Filters;

var builder = WebApplication.CreateBuilder(args);

var container = builder.Services;

container.AddControllers(config =>
{
    config.Filters.Add<AuthFilter>();
});

container.AddSignalR();

container.AddMemoryCache();

container.Configure<SignConfig>(builder.Configuration.GetSection(nameof(SignConfig)));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.MapHub<LoginHub>("/loginHub");

app.Run();
