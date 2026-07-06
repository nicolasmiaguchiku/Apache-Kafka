using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Consumer
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ILogger<ConsumerService> _logger;
        private readonly ConsumerConfig _consumerConfig;

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;

            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "Group 1",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando mensagem");
            _consumer.Subscribe("Example1");

            while (!stoppingToken.IsCancellationRequested)
            {

                await Task.Run(() =>
                 {
                     ConsumeResult<Ignore, string> result = _consumer.Consume(stoppingToken);

                     PersonModel? person = JsonSerializer.Deserialize<PersonModel>(result.Message.Value);

                     _logger.LogInformation($"GroupId: Group 1 Messagem: {result.Message.Value}");

                     _logger.LogInformation(person?.ToString());
                 });
            }
        }


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            _logger.LogInformation("Aplicação parou, conexão fechada");
            return Task.CompletedTask;
        }
    }
}
