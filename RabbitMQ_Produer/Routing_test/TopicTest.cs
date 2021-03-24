using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;

namespace RabbitMQ_Produer.Routing_test
{
    class TopicTest
    {
        public static void Topic_Test()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                 
                    model.QueueDeclare(queue: "TopicTestMessage1", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    model.QueueDeclare(queue: "TopicTestMessage2", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    model.QueueDeclare(queue: "TopicTestMessage3", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    model.QueueDeclare(queue: "TopicTestMessage4", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    model.ExchangeDeclare(exchange:"TopicExchangTest",type:ExchangeType.Topic,durable:true,autoDelete:false,arguments:null);

                    model.QueueBind(queue: "TopicTestMessage1", exchange: "TopicExchangTest", routingKey: "*.one", arguments: null);
                    model.QueueBind(queue: "TopicTestMessage2", exchange: "TopicExchangTest", routingKey: "*.two", arguments: null);
                    model.QueueBind(queue: "TopicTestMessage3", exchange: "TopicExchangTest", routingKey: "three.#", arguments: null);
                    model.QueueBind(queue: "TopicTestMessage4", exchange: "TopicExchangTest", routingKey: "*.four", arguments: null);

                    for (int i = 0; i < 100; i++)
                    {

                        model.BasicPublish(exchange: "TopicExchangTest", routingKey: "one.two.three.four", basicProperties: null, body: Encoding.UTF8.GetBytes($"Fanout发送广播消息{ i }"));

                            Console.WriteLine($"广播消息--{i}  已发送");
                     
                    }
                }
               
            }
        
        }
    }
}
