namespace BeforeTheScholarship.RabbitMq;

public delegate Task OnMessageReceive<T>(T action);
public interface IRabbitMqService
{
    Task Subscribe<T>(string queueName, OnMessageReceive<T> onReceive);
    Task PushAsync<T>(string queueName, T data, double delay);
}