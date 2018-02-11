using Prism.Commands;
using Akka.Actor;
using AkkaPrismDemo.Module.Stocks.ActorModels.Actors.UI;
using AkkaPrismDemo.Module.Stocks.ActorModels.Messages;
using log4net;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace AkkaPrismDemo.Module.Stocks.ViewModels
{
    public sealed class StockToggleButtonViewModel : ViewModel
    {

        /// <summary>
        /// The view name.
        /// </summary>
        public const string ViewName = "StockToggleButton";
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly static ILog Log = LogManager.GetLogger(typeof(StockToggleButtonViewModel));

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="unityContainer"></param>
        public StockToggleButtonViewModel(IRegionManager regionManager, IUnityContainer unityContainer) : base(regionManager, unityContainer)
        {
            if (Log.IsDebugEnabled) Log.DebugFormat("StockToggleButtonViewModel.ctor");
            ToggleStockCommand = new DelegateCommand(ToggleStock);
        }

        /// <summary>
        /// The button text to display.
        /// </summary>
        public string ButtonText { get; private set; }

        /// <summary>
        /// The stock coordinator actor.
        /// </summary>
        public IActorRef StocksCoordinatorActorRef { get; private set; }

        /// <summary>
        /// The stock symbol.
        /// </summary>
        public string StockSymbol { get; private set; }

        /// <summary>
        /// The stock toggle button actor.
        /// </summary>
        public IActorRef StockToggleButtonActorRef { get; private set; }

        /// <summary>
        /// The command to toggle the stock.
        /// </summary>
        public DelegateCommand ToggleStockCommand { get; }

        /// <summary>
        /// See <see cref="INavigationAware.IsNavigationTarget" />
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // Ensure new instances are created
            return false;
        }

        /// <summary>
        /// See <see cref="INavigationAware.OnNavigatedTo" />
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (Log.IsDebugEnabled) Log.DebugFormat("OnNavigatedTo");
            if (Log.IsDebugEnabled) Log.DebugFormat(" - parameters = {0}", navigationContext.Parameters);
            base.OnNavigatedTo(navigationContext);
            // Save the symbol
            StockSymbol = (string)navigationContext.Parameters["StockSymbol"];
            if (Log.IsDebugEnabled) Log.DebugFormat(" - stock symbol = '{0}'", StockSymbol);
            // Get the coordinator
            StocksCoordinatorActorRef = (IActorRef)navigationContext.Parameters["StocksCoordinatorActor"];
            if (Log.IsDebugEnabled) Log.DebugFormat(" - stock coordinator actor = {0}", StocksCoordinatorActorRef);
            // Create a stock toggle button actor
            StockToggleButtonActorRef = UnityContainer.Resolve<ActorSystem>()
                                                .ActorOf(Props.Create(() => new StockToggleButtonActor(StocksCoordinatorActorRef, this, StockSymbol)));
            // Start in "Off"
            UpdateButtonTextToOff();
        }

        /// <summary>
        /// Toggles the stock.
        /// </summary>
        private void ToggleStock()
        {
            if (Log.IsDebugEnabled) Log.DebugFormat("ToggleStock - StockSymbol = {0}", StockSymbol);
            StockToggleButtonActorRef?.Tell(new ToggleStockMessage());
        }

        /// <summary>
        /// Updates the button text.
        /// </summary>
        private void UpdateButtonText(bool isOn)
        {
            ButtonText = $"{StockSymbol} ({(isOn ? "on" : "off")})";
            RaisePropertyChanged(ButtonText);
        }

        /// <summary>
        /// Updates the button text to "Off"
        /// </summary>
        public void UpdateButtonTextToOff()
        {
            UpdateButtonText(false);
        }

        /// <summary>
        /// Updates the button text to "On"
        /// </summary>
        public void UpdateButtonTextToOn()
        {
            UpdateButtonText(true);
        }

    }
}