using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ;


namespace RabbitMQ_Produer.Routing_test
{
    class FanoutTest
    {
        public static void Fangout_Test()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";

            using(IConnection connection = factory.CreateConnection())
            {
                using (IModel model =connection.CreateModel())
                {
                    model.QueueDeclare(queue: "FanoutMessage1", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    model.QueueDeclare(queue: "FanoutMessage2", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    model.QueueDeclare(queue: "FanoutMessage3", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    model.QueueDeclare(queue: "FanoutMessage4", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    model.ExchangeDeclare(exchange: "FanoutExchangMessage", type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);

                    model.QueueBind(queue: "FanoutMessage1", exchange: "FanoutExchangMessage", routingKey: string.Empty, arguments: null);

                    model.QueueBind(queue: "FanoutMessage2", exchange: "FanoutExchangMessage", routingKey: string.Empty, arguments: null);

                    model.QueueBind(queue: "FanoutMessage3", exchange: "FanoutExchangMessage", routingKey: string.Empty, arguments: null);

                    model.QueueBind(queue: "FanoutMessage4", exchange: "FanoutExchangMessage", routingKey: string.Empty, arguments: null);

                    for (int i = 0; i < 100; i++)
                    {
                        model.BasicPublish(exchange: "FanoutExchangMessage", routingKey: string.Empty, basicProperties: null, body: Encoding.UTF8.GetBytes($"Fanout发送广播消息{ i }"));
                        Console.WriteLine($"广播消息--{i}  已发送");
                      
                    }
                }
            }
        }
    }
}
