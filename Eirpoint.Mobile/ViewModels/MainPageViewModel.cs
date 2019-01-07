using Eirpoint.Mobile.Core.Interfaces;
using Platform.Ioc.Injection;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Constructors

        public MainPageViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            //title
            Title = "Eirpoint Mobile - Zebra";

            //products button action
            PerformProductsCommand = new DelegateCommand(PerformProducts);

            //initialize progress bar
            PbProducts = 0.0;
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

        #endregion

        #region Delegates

        public DelegateCommand PerformProductsCommand { get; set; }

        #endregion

        #region Methods        

        private async void PerformProducts()
        {
            var product = await Injector.Resolver<IProductsApiCore>().GetProductsByPaging(UpdateProgressBar);    
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

        #endregion
    }
}
