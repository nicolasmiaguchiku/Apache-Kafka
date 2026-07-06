using Confluent.Kafka;
using Producer;
using System.Text.Json;

namespace Producer
{
    public class ProducerService
    {
        private readonly IConfiguration _settings;
        private readonly ProducerConfig _producerConfig;
        private readonly ILogger<ProducerService> _logger;

        public ProducerService(IConfiguration configuration, ILogger<ProducerService> logger)
        {
            _settings = configuration;
            _logger = logger;

            string? bootstrap = _settings.GetSection("KafkaSettings").GetSection("BootstrapServer").Value;

            _producerConfig = new ProducerConfig()
            {
                BootstrapServers = bootstrap,
            };
        }

        public async Task<string> SendMessage(string message)
        {
            string? topic = _settings.GetSection("KafkaSettings").GetSection("TopicName").Value;

            try
            {
                using (IProducer<Null, string> producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    DeliveryResult<Null, string> result = await producer.ProduceAsync(topic: topic, new() { Value = message });
                    _logger.LogInformation($"{result.Status.ToString()} - {message}");

                    return $"{result.Status.ToString()} - {message}";
                }
            }
            catch
            {
                _logger.LogError("Erro ao enviar mensagem");

                return "Erro ao enviar mensagem";
            }
        }

        public async Task<string> SendPeson(PersonModel person)
        {
            string? topic = _settings.GetSection("KafkaSettings").GetSection("TopicName").Value;

            string message = JsonSerializer.Serialize(person);

            try
            {
                using (IProducer<Null, string> producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    DeliveryResult<Null, string> result = await producer.ProduceAsync(topic: topic, new() { Value = message });
                    _logger.LogInformation($"{result.Status.ToString()} - {message}");

                    return $"{result.Status.ToString()} - {message}";
                }
            }
            catch
            {
                _logger.LogError("Erro ao enviar mensagem");

                return "Erro ao enviar mensagem";
            }
        }
    }
}
