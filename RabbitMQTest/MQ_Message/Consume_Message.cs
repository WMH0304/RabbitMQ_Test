using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQTest.MQ_Message
{
    class Consume_Message
    {
        public static void ConsumeMessage()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//MQ服务地址
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码
            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        EventingBasicConsumer consumer = new EventingBasicConsumer(model);

                        //触发接收消息的事件
                        consumer.Received += (model, ea) =>
                         {
                             var by = ea.Body;
                             var message = Encoding.UTF8.GetString(by.ToArray()) ;
                             Console.WriteLine($"消费消息{message}");
                         };
                        model.BasicConsume(queue: "MyMessage", autoAck: true, consumer: consumer);

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
