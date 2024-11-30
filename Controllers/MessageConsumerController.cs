using KafkaConsumerApp.Models;
using KafkaConsumerApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaConsumerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageConsumerController : ControllerBase
    {
        public MessageConsumerController()
        {

        }

        //http://localhost:5026/api/MessageConsumer
        [HttpGet]
        public string ConsumeMessage()
        {
            var messageQueue = new ConcurrentMessageQueue();
            var messageProcessor = new MessageProcessor();

            // Populate the queue with messages
            for (int i = 0; i < 10000; i++)
            {
                messageQueue.Enqueue($"Message {i}");
            }

            // Create and start tasks to process messages asynchronously
            var tasks = new List<Task>();
            while (messageQueue.TryDequeue(out var message))
            {
                tasks.Add(Task.Run(async () =>
                    await messageProcessor.ProcessMessageAsync(message)));
            }

            // Wait for all tasks to complete
            //Task.WhensAll(tasks);

            Console.WriteLine("All messages processed.");
            return "true";
        }
    }
}
