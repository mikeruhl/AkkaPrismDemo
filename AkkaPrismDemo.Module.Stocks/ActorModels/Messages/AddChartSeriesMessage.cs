namespace AkkaPrismDemo.Module.Stocks.ActorModels.Messages {

	internal class AddChartSeriesMessage {

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="stockSymbol"></param>
		public AddChartSeriesMessage( string stockSymbol ) {
			this.StockSymbol = stockSymbol;
		}

		/// <summary>
		/// The stock symbol.
		/// </summary>
		public string StockSymbol { get; }

	}

}
