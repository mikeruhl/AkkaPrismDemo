using Akka.Actor;

namespace AkkaPrismDemo.Module.Stocks.ActorModels.Messages {

	internal sealed class SubscribeToNewStockPricesMessage {

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="subscriber"></param>
		public SubscribeToNewStockPricesMessage( IActorRef subscriber ) {
			this.Subscriber = subscriber;
		}

		/// <summary>
		/// The subscriber.
		/// </summary>
		public IActorRef Subscriber { get; }
	}

}
