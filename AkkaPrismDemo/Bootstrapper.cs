using AkkaPrismDemo.Views;
using System.Windows;
using AkkaPrismDemo.Module.Stocks;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using log4net;

namespace AkkaPrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(Bootstrapper));
        protected override DependencyObject CreateShell()
        {
            if (_log.IsDebugEnabled) _log.DebugFormat("CreateShell");
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            if (_log.IsDebugEnabled) _log.DebugFormat("InitializeShell");
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(StocksModule));
        }

    }
}
