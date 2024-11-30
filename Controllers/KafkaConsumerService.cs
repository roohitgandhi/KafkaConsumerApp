using Confluent.Kafka;
using System.Collections.Concurrent;

namespace KafkaConsumer
{
    public class KafkaConsumerService
    {
        private readonly ConcurrentQueue<Message<string, string>> _messageQueue;
        private readonly int _workerThreadCount;

        public KafkaConsumerService(int workerThreadCount)
        {
            _messageQueue = new ConcurrentQueue<Message<string, string>>();
            _workerThreadCount = workerThreadCount;
        }

        public async Task StartAsync()
        {
            // Kafka consumer configuration
            var config = new ConsumerConfig
            {
                // ... Kafka configuration settings ...
            };

            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Subscribe("your_topic");

                // Start worker threads
                for (int i = 0; i < _workerThreadCount; i++)
                {
                    await Task.Run(async () =>
                    {
                        while (true)
                        {
                            if (_messageQueue.TryDequeue(out var message))
                            {
                                await ProcessMessageAsync(message);
                            }
                            else
                            {
                                await Task.Delay(100); // Adjust delay as needed
                            }
                        }
                    });
                }

                try
                {
                    while (true)
                    {
                        //var consumeResult = consumer.Consume();
                        //_messageQueue.Enqueue(consumeResult);
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
                finally
                {
                    consumer.Close();
                }
            }
        }

        private async Task ProcessMessageAsync(Message<string, string> message)
        {
            // Process the message here
            // ...


            Parallel.Invoke(
                async () => await StartAsync() 

                );
        }
    }
}