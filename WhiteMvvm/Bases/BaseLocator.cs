using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using WhiteMvvm.Services.Api;
using WhiteMvvm.Services.Cache;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Resolve;
using static WhiteMvvm.Services.DeviceUtilities.Mocks;
using WhiteMvvm.Services.Cache.SqliteService;

namespace WhiteMvvm.Bases
{
    public class BaseLocator
    {
        private static readonly Lazy<BaseLocator> Lazy = new Lazy<BaseLocator>(() => new BaseLocator());
        private readonly UnityContainer _container;
        private static bool _isRefreshing;

        public static BaseLocator Instance
        {
            get
            {
                if (!_isRefreshing)
                    return Lazy.Value;
                _isRefreshing = false;
                return new BaseLocator();
            }
        }
        public void RegisterBaseService()
        {
            _container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IReflectionResolve, ReflectionResolve>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IApiService, ApiService>(new ContainerControlledLifetimeManager());
            //_container.RegisterType<IShare, ShareService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPreferences, PreferencesService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IFileSystem, FileSystemService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISqliteService, SqliteService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IBrowser, BrowserService>(new ContainerControlledLifetimeManager());
        }
        private BaseLocator()
        {
            _container = new UnityContainer().AddExtension(new Diagnostic()) as UnityContainer;
            RegisterBaseService();
        }
        private void MocksUpdateInternal(bool useMocks)
        {
            // Change injected dependencies
            if (useMocks)
            {
                _container.RegisterType<IConnectivity, ConnectivityMockService>(new ContainerControlledLifetimeManager());
                _container.RegisterType<IDialogService, DialogMockService>(new ContainerControlledLifetimeManager());
            }
            else
            {
                _container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());
                _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            }
        }
        public T Resolve<T>() where T : class
        {
            return _container?.Resolve<T>();
        }
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
        public void Register<T>()
        {
            _container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }
        public void RefreshLocator()
        {
            _isRefreshing = true;
        }
        public void MocksUpdate(bool useMocks)
        {
            MocksUpdateInternal(useMocks);
        }
    }
}
