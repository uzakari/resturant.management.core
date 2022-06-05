namespace Domain.Settings;

public class QueueConfiguration
{
    public string QueueName { get; set; }
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
}
