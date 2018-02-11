using AkkaPrismDemo.Module.Stocks.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Akka.Actor;
using Akka.DI.Unity;
using AkkaPrismDemo.Core;
using AkkaPrismDemo.Module.Stocks.ExternalServices;
using AkkaPrismDemo.Module.Stocks.ViewModels;
using log4net;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace AkkaPrismDemo.Module.Stocks
{
    public class StocksModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;
        private readonly static ILog _log = LogManager.GetLogger(typeof(StocksModule));

        public StocksModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            if (_log.IsDebugEnabled) _log.DebugFormat("Initialize");
            // Create the actor system
            var actorSystem = ActorSystem.Create("StockActorSystem");
            // Create a dependency resolver
            var resolver = new UnityDependencyResolver(this._container, actorSystem);
            // Register the actor system with the container
            _container.RegisterInstance(actorSystem);
            // Register our random stock price service gateway to the interface
            _container.RegisterInstance<IStockPriceServiceGateway>(new RandomStockPriceServiceGateway());

            // Register our views
            _container.RegisterTypeForNavigation<StockToggleButton>(StockToggleButtonViewModel.ViewName);
            this._regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(StockChart));
        }
    }
}