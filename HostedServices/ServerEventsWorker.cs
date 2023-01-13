using Lib.AspNetCore.ServerSentEvents;

namespace WebApi.HostedServices;
public class ServerEventsWorker : BackgroundService
{
    private readonly IServerSentEventsService client;

    public ServerEventsWorker(IServerSentEventsService client)
    {
        this.client = client;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var clients = client.GetClients();
                    if (clients.Any())
                    {
                        // ServerSentEventUpdateDTO update = new ServerSentEventUpdateDTO()
                        // {
                        //     Type = "EVENT",
                        //     Id = 1,
                        //     Event = new EventForUserDTO()
                        // };

                        await client.SendEventAsync("keep-alive");

                        // await client.SendEventAsync(
                        //     new ServerSentEvent
                        //     {
                        //         Id = "number",
                        //         Type = "number",
                        //         Data = new List<string>
                        //         {
                        //             RandomNumberGenerator.GetInt32(1, 100).ToString()
                        //         }
                        //     }
                        // );
                    }

                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
            catch (TaskCanceledException)
            {
            }
        });
    }
}