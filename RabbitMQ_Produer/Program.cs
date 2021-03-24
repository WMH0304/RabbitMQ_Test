using System;
using RabbitMQ_Produer;
using static RabbitMQ_Produer.Producer_Message;
using static RabbitMQ_Produer.Routing_test.DirectTest;
using static RabbitMQ_Produer.Routing_test.FanoutTest;
using static RabbitMQ_Produer.Routing_test.TopicTest;

namespace RabbitMQ_Produer
{
    class Program
    {
        static void Main(string[] args)
        {
            // ProducerMessage();
            //Direct_test();
            //Fangout_Test();
            Topic_Test();
        }
      

    }
}
