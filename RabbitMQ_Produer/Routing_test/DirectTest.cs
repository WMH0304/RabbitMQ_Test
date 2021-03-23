using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;

namespace RabbitMQ_Produer.Routing_test
{
    class DirectTest
    {
        class LogMsg
        {
            public string Types { get; set; }

            public  byte [] Contents { get; set; }
        }

        public static void Direct_test()
        {
            var factory = new ConnectionFactory();
              factory.HostName = "localhost";//MQ服务地址
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                    //info  队列
                    model.QueueDeclare(queue: "Info_DirectExchangQueue",durable:true,exclusive:false,
                        autoDelete:false,arguments:null);
                  
                    model.QueueDeclare(queue: "Error_DirectEXchangQueue", durable:true,exclusive:false,autoDelete:false,arguments:null);
                    
                  model.QueueDeclare(queue: "Wra_DirectEXchangQueue",durable:true,
                      exclusive:false,autoDelete:false,arguments:null);

                  model.QueueDeclare(queue: "Debug_DirectEXchangQueue", durable: true,
                      exclusive: false, autoDelete: false, arguments: null);
                  

                    //声明路由
                    //exchange 路由名称 ，  type 路由类型 ， durable  是否持久化 ，autoDelete 是否自动删除， arguments  一个字典集合
                    model.ExchangeDeclare(exchange: "DirectExChange",type:ExchangeType.Direct,durable:true,autoDelete:false,arguments:null);


                    string[] message = new string[] {"Info", "Error", "Wra", "Debug" };

                   //绑定队列 并设置 routingkey
                   //info
                        model.QueueBind(queue: "Info_DirectExchangQueue",exchange: "DirectExChange",routingKey: "Info");
                    //Error
                    model.QueueBind(queue: "Error_DirectEXchangQueue", exchange: "DirectExChange", routingKey: "Error");
                    //Wra
                    model.QueueBind(queue: "Wra_DirectEXchangQueue", exchange: "DirectExChange", routingKey: "Wra");
                    //Debug
                    model.QueueBind(queue: "Debug_DirectEXchangQueue", exchange: "DirectExChange", routingKey: "Debug");

                    List<LogMsg> logMsgs = new List<LogMsg>();
                    for (int i = 0; i < 100; i++)
                    {
                        if (i%4 ==0)
                        {
                            logMsgs.Add(new LogMsg() { Types = "Info", Contents = Encoding.UTF8.GetBytes($"info--{i}") });
                        }
                        if (i % 4 == 1)
                        {
                            logMsgs.Add(new LogMsg() { Types = "Error", Contents = Encoding.UTF8.GetBytes($"Error--{i}") });
                        }
                        if (i % 4 == 2)
                        {
                            logMsgs.Add(new LogMsg() { Types = "Wra", Contents = Encoding.UTF8.GetBytes($"Wra--{i}") });
                        }
                        if (i % 4 == 3)
                        {
                            logMsgs.Add(new LogMsg() { Types = "Debug", Contents = Encoding.UTF8.GetBytes($"Debug--{i}") });
                        }
                    }

                    foreach (var item in logMsgs)
                    {

                        //exchange 路由名称  routingKey 消息key ,basicProperties 基本性质， body 内容
                        model.BasicPublish(exchange:"DirectExChange",routingKey:item.Types,basicProperties:null,body:item.Contents);
                        Console.WriteLine($"{Encoding.UTF8.GetString(item.Contents)}已发送消息");
                    }


                }
            }
        }
    }
}
