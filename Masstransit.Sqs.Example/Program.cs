using System;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using GreenPipes;
using MassTransit;
using MassTransit.AmazonSqsTransport;
using MassTransit.AmazonSqsTransport.Configuration;
using Newtonsoft.Json.Linq;

namespace masstran
{
    class Program
    {
        static void Main(string[] args)
        {
	        var sqsServiceUri = "http://localhost:4576";
	        var snsServiceUri = "http://localhost:4575";
	        var AccessKey = "foo";
	        var SecretKey = "bar";
	        var queue = "carina-sending-email";


	        var busControl = Bus.Factory.CreateUsingAmazonSqs(cfg =>
            {
                var host = cfg.Host(new Uri("amazonsqs://localhost:4576"), h =>
                {
                    h.AccessKey(AccessKey);
                    h.SecretKey(SecretKey);
                    h.Config(new AmazonSimpleNotificationServiceConfig { ServiceURL = snsServiceUri });
                    h.Config(new AmazonSQSConfig { ServiceURL = sqsServiceUri });
				});


                cfg.ReceiveEndpoint(host, queue, e =>
                {
					e.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
					e.UseMessageRetry(r => r.Immediate(5));
					e.Consumer<Consumer>();
					e.Consumer<FaultConsumer>();
                });
			});

	        var observer = new ReceiveObserver();
	        var handle = busControl.ConnectReceiveObserver(observer);

			busControl.Start();

			busControl.Publish(new SqsMessage { Msg = "Inner message"});

			Console.ReadLine();
        }
    }
}
