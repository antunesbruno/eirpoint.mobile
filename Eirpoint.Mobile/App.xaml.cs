using Eirpoint.Mobile.Core.Api;
using Eirpoint.Mobile.Core.Bll;
using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Interfaces;
using Eirpoint.Mobile.Datasource.Repository.Base;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared;
using Eirpoint.Mobile.Shared.Barcode;
using Eirpoint.Mobile.Shared.NativeInterfaces;
using Eirpoint.Mobile.ViewModels;
using Eirpoint.Mobile.Views;
using Platform.Ioc;
using Platform.Ioc.Injection;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Eirpoint.Mobile
{
    public partial class App : PrismApplication
    {      
    
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //register platform dependencies and all mobile dependencies
            RegisterDependencies.BuildDependencies(RegisterMobileDependencies);

            //register navigations
            RegisterNavigations(containerRegistry);

            //create database
            Injector.Resolver<IDatabaseHelper>().CreateDatabase();

            //create tables
            Injector.Resolver<IDatabaseHelper>().CreateTables();           
        }

        /// <summary>
        /// MVVM dependencies (Views / ViewModels)
        /// </summary>
        /// <param name="containerRegistry"></param>
        private void RegisterNavigations(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<BarcodeView, BarcodeViewViewModel>();
        }

        /// <summary>
        /// Dependencies of application
        /// </summary>
        private void RegisterMobileDependencies()
        {
            //datasource
            Injector.RegisterType<DatabaseHelper, IDatabaseHelper>();
            Injector.RegisterType<PersistenceBase<ProductsEntity>, IPersistenceBase<ProductsEntity>>();
            Injector.RegisterType<PersistenceBase<RangeHeaderLog>, IPersistenceBase<RangeHeaderLog>>();

            //core api
            Injector.RegisterType<ProductsApi, IProductsApiCore>();
            Injector.RegisterType<BarcodeProductsApi, IBarcodeProductsApiCore>();
            Injector.RegisterType<BasicDataApi, IBasicDataApiCore>(); 

            //core bll
            Injector.RegisterType<ProductsBll, IProductsBll>();

            //barcode
            Injector.RegisterType<BarcodeHandler, IBarCode>();

            //native interfaces
            Injector.RegisterType<NativeConnectivity, IConnectivity>();

        }
    }
}
