namespace KafkaConsumerApp.Services
{
    public class MessageProcessor
    {
        public async Task ProcessMessageAsync(string message)
        {
            // Simulate asynchronous I/O-bound operation
            await Task.Delay(100);

            // Process the message
            Console.WriteLine($"Processing message: {message}");
        }
    }
}
