using Eirpoint.Mobile.Core.Barcode;
using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Core.NativeInterfaces;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Platform.Ioc.Injection;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eirpoint.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;

        #endregion

        #region Constructors     

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
          : base(navigationService)
        {
            //dialog
            _dialogService = dialogService;

            //title
            Title = "Eirpoint Mobile - Zebra";

            //products button action
            PerformProductsCommand = new DelegateCommand(PerformProducts);

            //products button action
            SearchBarcodeCommand = new DelegateCommand(SearchBarcode);

            //initialize progress bar
            PbProducts = 0.0;

            //delegate result barcode read
            Injector.Resolver<IBarCode>().OnBarCodeRead += UpdateRead;
        }

        #endregion

        #region Properties

        private double _pbProducts;
        public double PbProducts
        {
            get { return _pbProducts; }
            set {
                SetProperty(ref _pbProducts, value);
                RaisePropertyChanged(nameof(PbProducts));
            }
        }

        private string _lbProducts;
        public string LbProducts
        {
            get { return _lbProducts; }
            set
            {
                SetProperty(ref _lbProducts, value);
                RaisePropertyChanged(nameof(LbProducts));
            }
        }

        private string _edtResultRead;
        public string EdtResultRead
        {
            get { return _edtResultRead; }
            set
            {
                SetProperty(ref _edtResultRead, value);
                RaisePropertyChanged(nameof(EdtResultRead));
            }
        }

        #endregion

        #region Delegates

        public DelegateCommand PerformProductsCommand { get; set; }
        public DelegateCommand SearchBarcodeCommand { get; set; }

        #endregion

        #region Methods        

        private async void PerformProducts()
        {
            var productsList = await Injector.Resolver<IProductsApiCore>().GetProductsByPaging(UpdateProgressBar);

            if (productsList.Count > 0)
            {
                //insert products in database
                Injector.Resolver<IProductsBll>().InsertAllProducts(productsList);

                //update progressbar
                UpdateProgressBar(100);

                //inform user about finish process
                await _dialogService.DisplayAlertAsync("Perform Products", "Products downloaded and saved successfully !", "OK");
            }
        }

        private async void SearchBarcode()
        {
            var entity = await Injector.Resolver<IBarcodeProductsApiCore>().GetBarcodeProductByCode("3414900916229");
        }

        private void UpdateProgressBar(int percent)
        {
            Task.Run(() =>
             {
                 decimal y = ((decimal)percent) / 100;
                 PbProducts = Convert.ToDouble(y);
                 LbProducts = percent.ToString() + "%";
             });
        }

        private async void UpdateRead(BarcodeReadArgs args)
        {
            if (!string.IsNullOrEmpty(args.BarCodeData))
            {
                EdtResultRead = args.BarCodeData + "\n \n";
            }
            else if (!string.IsNullOrEmpty(args.Message))
            {
                EdtResultRead = args.Message + "\n \n";
            }
            else
            {
                EdtResultRead = "Barcode not found...: " + args + "\n ";
            }
        }

        #endregion
    }
}
