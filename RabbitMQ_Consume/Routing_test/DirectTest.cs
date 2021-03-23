using RabbitMQ.Client;
using RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Events;

namespace RabbitMQ_Consume.Routing_test
{
    class DirectTest
    {
        public static void Direct_test()
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


                        consumer.Received += (model, ea) =>
                        {
                            var by = ea.Body;
                            var mess = Encoding.UTF8.GetString(by.ToArray());
                        };
                       
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
