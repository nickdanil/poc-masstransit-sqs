using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace masstran
{
	public class ReceiveObserver : IReceiveObserver
	{
		public Task PreReceive(ReceiveContext context)
		{
			Console.Write("");
			return Task.FromResult("");
		}

		public Task PostReceive(ReceiveContext context)
		{
			// called after the message has been received and processed
			return Task.FromResult("");
		}

		public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
			where T : class
		{
			// called when the message was consumed, once for each consumer
			return Task.FromResult("");
		}

		public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
		{
			// called when the message is consumed but the consumer throws an exception
			return Task.FromResult("");
		}

		public Task ReceiveFault(ReceiveContext context, Exception exception)
		{
			// called when an exception occurs early in the message processing, such as deserialization, etc.
			return Task.FromResult("");
		}
	}
}
