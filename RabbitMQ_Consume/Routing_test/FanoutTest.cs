using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ;
using RabbitMQ.Client.Events;

namespace RabbitMQ_Consume.Routing_test
{
    class FanoutTest
    {
        public static void Fangout_Test()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                    try
                    {
                        EventingBasicConsumer consumer = new EventingBasicConsumer(model);
                        consumer.Received += (model, ea) =>
                        {
                            var by = ea.Body;
                            var mg = Encoding.UTF8.GetString(by.ToArray());
                            Console.WriteLine($"接受广播消息{mg}");
                        };
                        for (int i = 0; i < 100; i++)
                        {

                            model.BasicConsume(queue: "FanoutMessage1", autoAck: true, consumer: consumer);

                            model.BasicConsume(queue: "FanoutMessage2", autoAck: true, consumer: consumer);

                            model.BasicConsume(queue: "FanoutMessage3", autoAck: true, consumer: consumer);
                           
                            model.BasicConsume(queue: "FanoutMessage4", autoAck: true, consumer: consumer);

                    

                        }



                    }
                    catch (Exception)
                    {

                        throw;
                    }
                } 
            }
        }
    }
}
