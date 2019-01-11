using Acr.UserDialogs;
using Eirpoint.Mobile.Core.Barcode;
using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Core.NativeInterfaces;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Platform.Ioc.Injection;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        IProgressDialog _progressDialog;

        #endregion

        #region Constructors     

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
          : base(navigationService)
        {
            //dialog
            _dialogService = dialogService;

            //title
            Title = "Eirpoint Mobile - Zebra";

            //label results
            LbResults = "Status Barcode and Read Value :";

            //products button action
            PerformProductsCommand = new DelegateCommand(PerformProducts);

            //products button action
            SearchBarcodeCommand = new DelegateCommand(SearchBarcode);

            //initialize progress bar
            PbProducts = 0.0;

            //delegate result barcode read
            Injector.Resolver<IBarCode>().OnBarCodeRead += UpdateRead;

            //indicator
            IsRunning = false;
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

        private string _lbResults;
        public string LbResults
        {
            get { return _lbResults; }
            set
            {
                SetProperty(ref _lbResults, value);
                RaisePropertyChanged(nameof(_lbResults));
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

        private BarcodesEntity _barcodesEntity;
        public BarcodesEntity BarcodesEntity
        {
            get { return _barcodesEntity; }
            set
            {
                SetProperty(ref _barcodesEntity, value);
                RaisePropertyChanged(nameof(BarcodesEntity));
            }
        }

        private string _txtProductsFind;
        public string TxtProductsFind
        {
            get { return _txtProductsFind; }
            set
            {
                SetProperty(ref _txtProductsFind, value);
                RaisePropertyChanged(nameof(TxtProductsFind));
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                SetProperty(ref _isRunning, value);
                RaisePropertyChanged(nameof(IsRunning));
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
            using (_progressDialog = UserDialogs.Instance.Progress("Downloading Products", null, null, true, MaskType.Black))
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
        }

        private async void SearchBarcode()
        {
            using (var Dialog = UserDialogs.Instance.Loading("Searching Barcode", null, null, true, MaskType.Black))
            {
                BarcodesEntity = new BarcodesEntity();

                var entity = await Injector.Resolver<IBarcodeProductsApiCore>().GetBarcodeProductByCode(TxtProductsFind);

                if (entity?.Barcode != null)
                    BarcodesEntity = entity;
            }
        }

        private void UpdateProgressBar(int percent)
        {
            Task.Run(() =>
             {
                 _progressDialog.PercentComplete = percent;
             });
        }

        private void UpdateRead(BarcodeReadArgs args)
        {
            if (!string.IsNullOrEmpty(args.BarCodeData))
            {
                TxtProductsFind = args.BarCodeData;
                EdtResultRead = args.Message + "\n \n";
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
