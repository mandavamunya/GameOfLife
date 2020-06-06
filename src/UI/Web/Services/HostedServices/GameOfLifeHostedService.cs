using Core.Extensions;
using Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Web.Services.HostServices
{
    public class GameOfLifeHostedService : BackgroundService
    {
        private readonly ILogger<GameOfLifeHostedService> _logger;
        private readonly IOptions<TimerServiceConfiguration> _options;
        private readonly IHubContext<BoardMessagesHub> _hubContext;

        public GameOfLifeHostedService(
          ILoggerFactory loggerFactory,
          IOptions<TimerServiceConfiguration> options,
          IHubContext<BoardMessagesHub> hubContext)
        {
            _logger = loggerFactory.CreateLogger<GameOfLifeHostedService>();
            _options = options;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var board = Game.Board;
                _logger.LogInformation($"Sending Updated Board {board.Count}");

                string json = null;
                try
                {

                    json = JsonConvert.SerializeObject(board, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    Console.WriteLine(json);
                    await _hubContext.Clients.All.SendAsync("UpdatedBoard", json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(json);
                    Console.WriteLine(e);
                }
                await Task.Delay(TimeSpan.FromMilliseconds(_options.Value.Period), stoppingToken);
            }
        }
    }
}
