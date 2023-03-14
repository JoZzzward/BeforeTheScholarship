using BeforeTheScholarship.Services.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace BeforeTheScholarship.Services.RabbitMqService;

public class RabbitMqService : IRabbitMqService, IDisposable
{
    private readonly object connectionLock = new();
    private IConnection connection;

    private IModel channel;
    private const string exchangeName = "delayedDebtExchange";
    
    private readonly RabbitMqSettings _settings;

    public RabbitMqService(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public async Task Subscribe<T>(string queueName, OnMessageReceive<T> onReceive)
    {
        if (onReceive == null)
            return;

        await RegisterListener(queueName, async (_, eventArgs) =>
        {
            var channel = GetChannel();
            try
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var obj = JsonSerializer.Deserialize<T>(message ?? "");

                await onReceive(obj!);
                channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch 
            {
                channel.BasicNack(eventArgs.DeliveryTag, false, false);
            }
        });
    }

    private IModel GetChannel() => channel;

    private async Task Publish<T>(string queueName, T data, double delay)
    {
        Connect();

        AddQueue(queueName);

        IBasicProperties props = channel.CreateBasicProperties();
        props.Headers = new Dictionary<string, object>
        {
            { "x-delay", delay }
        };

        var json = JsonSerializer.Serialize<object>(data, new JsonSerializerOptions());

        var message = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchangeName, queueName, props, message);
    }

    private async Task RegisterListener(string queueName, EventHandler<BasicDeliverEventArgs> onReceive)
    {
        Connect();

        AddQueue(queueName);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += onReceive;

        channel.BasicConsume(queueName, false, consumer);
    }

    public async Task PushAsync<T>(string queueName, T data, double delay)
    {
        await Publish(queueName, data, delay);
    }

    private void AddQueue(string queueName)
    {
        Connect();

        channel.QueueDeclare(queueName, true, false, false, null);

        channel.QueueBind(queueName, exchangeName, queueName);
    }

    private void Connect()
    {
        lock (connectionLock)
        {
            if (connection?.IsOpen ?? false)
                return;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(_settings.Uri),
                UserName = _settings.UserName,
                Password = _settings.Password,

                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            };

            var retriesCount = 0;
            while (retriesCount < 15)
                try
                {
                    if (connection == null)
                    {
                        connection = factory.CreateConnection();
                    }

                    if (channel == null)
                    {
                        channel = connection.CreateModel();
                        channel.BasicQos(0, 1, false);
                    }
                    DeclareExchange();
                    break;
                }
                catch (BrokerUnreachableException)
                {
                    Task.Delay(1000).Wait();

                    retriesCount++;
                }
        }
    }

    private void DeclareExchange()
    {
        var args = new Dictionary<string, object>
        {
            { "x-delayed-type", "direct" }
        };

        channel.ExchangeDeclare(exchangeName, "x-delayed-message", true, false, args);
    }

    public void Dispose()
    {
        connection?.Dispose();
        channel?.Dispose();
    }
}
