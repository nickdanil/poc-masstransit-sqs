using Amazon.SQS.Model;
using MassTransit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace masstran
{
    public class Consumer: IConsumer<SqsMessage>
	{
		public async Task Consume(ConsumeContext<SqsMessage> context)
		{
			await Console.Out.WriteLineAsync($"Updating customer: {context}");

			// update the customer address
		}
	}

    public class FaultConsumer : IConsumer<Fault<SqsMessage>>
    {
	    public async Task Consume(ConsumeContext<Fault<SqsMessage>> context)
	    {
		    await Console.Out.WriteLineAsync($"Updating customer: {context}");
	    }
    }

    public interface Fault<T>
	    where T : class
    {
	    Guid FaultId { get; }
	    Guid? FaultedMessageId { get; }
	    DateTime Timestamp { get; }
	    ExceptionInfo[] Exceptions { get; }
	    HostInfo Host { get; }
	    T Message { get; }
    }

	public class SqsMessage
	{
		public string Msg { get; set; }
	}
}