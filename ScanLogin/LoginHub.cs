using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace ScanLogin
{
    public class LoginHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<LoginHub> _logger;

        public LoginHub(IMemoryCache memoryCache, ILogger<LoginHub> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public void Send(string client)
        {
            _logger.LogInformation("建立二维码关系："+client);
            _memoryCache.Set(client, Context.ConnectionId, DateTimeOffset.Now.AddMinutes(2));
        }
    }
}
