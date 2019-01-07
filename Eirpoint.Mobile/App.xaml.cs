using Prism;
using Prism.Ioc;
using Eirpoint.Mobile.ViewModels;
using Eirpoint.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Platform.Ioc;
using Platform.Ioc.Injection;
using Platform.Shared.Contract;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Interfaces;
using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Core.Api;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Eirpoint.Mobile
{
    public partial class App
    {      

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //register platform dependencies and all mobile dependencies
            PlatformDependencies.BuildDependencies(RegisterMobileDependencies);

            //register navigations
            RegisterNavigations(containerRegistry);

            //create database
            Injector.Resolver<IDatabaseHelper>().CreateDatabase();

            //create tables
            Injector.Resolver<IDatabaseHelper>().CreateTables();       
        }

        private void RegisterNavigations(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<BarcodeView, BarcodeViewViewModel>();
        }

        private void RegisterMobileDependencies()
        {
            //datasource
            Injector.RegisterType<DatabaseHelper, IDatabaseHelper>();

            //core
            Injector.RegisterType<ProductsApi, IProductsApiCore>();
            
        }
    }
}
