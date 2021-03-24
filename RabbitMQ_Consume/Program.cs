using System;
using static RabbitMQ_Consume.Consume_Message;
using static RabbitMQ_Consume.Routing_test.DirectTest;
using static RabbitMQ_Consume.Routing_test.FanoutTest;

namespace RabbitMQ_Consume
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConsumeMessage();
            // Direct_test();
            Fangout_Test();
        }
    }
}
