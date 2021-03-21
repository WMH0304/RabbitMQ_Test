using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ_Produer
{
    class Producer_Message
    {
        //nuget RabbitMQ.Client 程序包
        public static void ProducerMessage()
        {
            //链接 MQ
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";//MQ服务地址
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码
            using (IConnection connection = factory.CreateConnection())//创建链接
            {
                using (IModel model = connection.CreateModel())//创建通信管道
                {
                    //durable：string队列，durable：bool持久，exclusive：bool独占，autoDelete：bool自动删除，arguments：IDictionary<string, object>字典对象用来存发数据
                    model.QueueDeclare(queue: "MyMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);//在管道中声明队列

                   
                    model.ExchangeDeclare(exchange: "MyExChangMessage", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);//声明路由，相当于是一个信标

                    model.QueueBind(queue: "MyMessage", exchange: "MyExChangMessage", routingKey: String.Empty, arguments: null);//绑定路由管道，

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("生产者准备就绪");

                    int i = 1;
                    while (true)
                    {
                        string message = $"消息队列{i}";
                        //转换编码格式 ⭐ RabbitMQ 消息传递只能通过 byte 数组传递
                        byte[] by = Encoding.UTF8.GetBytes(message);
                        model.BasicPublish(exchange: "MyExChangMessage", routingKey: string.Empty, basicProperties: null, body: by);//发送路由消息 到队列中
                        Console.WriteLine($"{message}已发送");
                        i++;
                        Thread.Sleep(200);
                    }






                }
            }
        }
    }
}
