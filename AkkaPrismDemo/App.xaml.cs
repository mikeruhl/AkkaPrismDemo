using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using log4net.Config;

namespace AkkaPrismDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(App));

        protected override void OnStartup(StartupEventArgs e)
        {

            XmlConfigurator.Configure();
            if (_log.IsDebugEnabled) _log.DebugFormat("OnStartup");

            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
