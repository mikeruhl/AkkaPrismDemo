using System;
using Akka.Actor;
using AkkaPrismDemo.Module.Stocks.ActorModels.Messages;
using AkkaPrismDemo.Module.Stocks.ExternalServices;
using log4net;

namespace AkkaPrismDemo.Module.Stocks.ActorModels.Actors {

	/// <summary>
	/// Looks up stock prices upon request.
	/// </summary>
	internal sealed class StockPriceLookupActor : ReceiveActor {

		/// <summary>
		/// The logger.
		/// </summary>
		private readonly static ILog _log = LogManager.GetLogger( typeof( StockPriceLookupActor ) );

		/// <summary>
		/// The stock price service.
		/// </summary>
		private readonly IStockPriceServiceGateway _stockPriceServiceGateway;

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="stockPriceServiceGateway"></param>
		public StockPriceLookupActor( IStockPriceServiceGateway stockPriceServiceGateway ) {
			if ( _log.IsDebugEnabled ) _log.DebugFormat( "StockPriceLookupActor.ctor - gateway = '{0}'", stockPriceServiceGateway );
			this._stockPriceServiceGateway = stockPriceServiceGateway;
			this.Receive( (Action<RefreshStockPriceMessage>) this.LookupStockPrice );
		}

		/// <summary>
		/// Looks up a stock price.
		/// </summary>
		/// <param name="message"></param>
		private void LookupStockPrice( RefreshStockPriceMessage message ) {
			var latestPrice = this._stockPriceServiceGateway.GetLatestPrice( message.StockSymbol );
			this.Sender.Tell( new UpdatedStockPriceMessage( latestPrice ) );
		}

	}

}
