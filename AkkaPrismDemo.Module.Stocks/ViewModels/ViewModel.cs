using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;

namespace AkkaPrismDemo.Module.Stocks.ViewModels
{
    public abstract class ViewModel : BindableBase, INavigationAware
    {

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly static ILog _log = LogManager.GetLogger(typeof(ViewModel));

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="unityContainer"></param>
        protected ViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.RegionManager = regionManager;
            this.UnityContainer = unityContainer;
        }

        /// <summary>
        /// The region manager.
        /// </summary>
        protected IRegionManager RegionManager { get; }

        /// <summary>
        /// The unity container.
        /// </summary>
        protected IUnityContainer UnityContainer { get; }

        #region INavigationAware

        /// <summary>
        /// See <see cref="INavigationAware.IsNavigationTarget" />
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// See <see cref="INavigationAware.OnNavigatedFrom" />
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// See <see cref="INavigationAware.OnNavigatedTo" />
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        #endregion
    }
}
