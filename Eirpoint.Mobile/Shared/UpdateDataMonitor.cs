using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared.Enumerators;
using Eirpoint.Mobile.Shared.NativeInterfaces;
using Platform.Ioc.Injection;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Shared
{
    public class UpdateDataMonitor
    {
        #region Fields

        private HttpClient _httpClient;

        #endregion

        /// <summary>
        /// Internect connection
        /// </summary>
        private bool IsConnected { get { return Injector.Resolver<IConnectivity>().IsConnected(); } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Action<int> callback;
      
        /// <summary>
        /// 
        /// </summary>        
        public async void UpdateMonitorAction()
        {
            Debug.WriteLine("<<< Starting PushMonitor...");
            do
            {
                try
                {
                    Debug.WriteLine("<<< Check Is Connected... ");                    

                    if (IsConnected && CommonPlatform.Configuration.HasBasicData)
                    {
                        //declare helper
                        var httpHelper = new HttpHelper();

                        // Loop until the whole query is fulfilled.
                        foreach (var endpoint in EntityResourcesHelper.GetValues())
                        {                             
                            switch (endpoint.Key)
                            {
                                case AltoposResourcesEnum.CUSTOMER:
                                    {
                                        await httpHelper.SynchronizeBackground<CustomersEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.DEPARTMENT:
                                    {
                                        await httpHelper.SynchronizeBackground<DepartmentEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.DISCOUNT:
                                    {
                                        await httpHelper.SynchronizeBackground<DiscountsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.PAYMENT_CATEGORY:
                                    {
                                        await httpHelper.SynchronizeBackground<PaymentsCategoriesEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.GROUP_LIST:
                                    {
                                        await httpHelper.SynchronizeBackground<GroupsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.POS_STATION:
                                    {
                                        await httpHelper.SynchronizeBackground<PosStationsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.PRODUCT:
                                    {
                                        await httpHelper.SynchronizeBackground<ProductsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.PRODUCT_BARCODE:
                                    {
                                        await httpHelper.SynchronizeBackground<ProductBarCodesEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.PROMOTIONS:
                                    {
                                        await httpHelper.SynchronizeBackground<PromotionsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.REASON:
                                    {
                                        await httpHelper.SynchronizeBackground<ReasonsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.RECEIPT:
                                    {
                                        await httpHelper.SynchronizeBackground<ReceiptTemplatesEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.STOCKING_ATTRIBUTE_TYPE:
                                    {
                                        await httpHelper.SynchronizeBackground<StockingAttributeTypeEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.STOCK_LOCATION:
                                    {
                                        await httpHelper.SynchronizeBackground<StockLocationsEntity>(endpoint.Value);
                                        break;
                                    }
                                case AltoposResourcesEnum.USER_LIST:
                                    {
                                        await httpHelper.SynchronizeBackground<UsersEntity>(endpoint.Value);
                                        break;
                                    }
                            }
                        }
                    }

                    Debug.WriteLine("<<< Push Monitor waiting 30s to call receive data");

                    Task.Delay(30 * 1000).Wait();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            while (IsRunning);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        public virtual void RegisterCallback(Action<int> callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void UnregisterCallback()
        {
            this.callback = null;
        }
    }
}
