using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared.Enumerators;
using Eirpoint.Mobile.Shared.NativeInterfaces;
using Platform.Ioc.Injection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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
        public void UpdateMonitorAction()
        {
            Debug.WriteLine("<<< Starting PushMonitor...");
            do
            {
                try
                {
                    Debug.WriteLine("<<< Check if is in transaction");

                    if (IsConnected)
                    {
                        //declare helper
                        var httpHelper = new HttpHelper();

                        // Loop until the whole query is fulfilled.
                        foreach (var endpoint in EntityResourcesHelper.GetValues())
                        {
                            //complement endpoint (in this case because is using a generic method in refit)
                            _httpClient = Endpoints.BaseEirpointHttpClient(endpoint.Value);

                            switch (endpoint.Key)
                            {
                                case AltoposResourcesEnum.CUSTOMER:
                                    {
                                        httpHelper.SynchronizeBackground<CustomersEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.DEPARTMENT:
                                    {
                                        httpHelper.SynchronizeBackground<DepartmentEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.DISCOUNT:
                                    {
                                        httpHelper.SynchronizeBackground<DiscountsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.PAYMENT_CATEGORY:
                                    {
                                        httpHelper.SynchronizeBackground<PaymentsCategoriesEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.GROUP_LIST:
                                    {
                                        httpHelper.SynchronizeBackground<GroupsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.POS_STATION:
                                    {
                                        httpHelper.SynchronizeBackground<PosStationsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.PRODUCT:
                                    {
                                        httpHelper.SynchronizeBackground<ProductsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.PRODUCT_BARCODE:
                                    {
                                        httpHelper.SynchronizeBackground<ProductBarCodesEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.PROMOTIONS:
                                    {
                                        httpHelper.SynchronizeBackground<PromotionsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.REASON:
                                    {
                                        httpHelper.SynchronizeBackground<ReasonsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.RECEIPT:
                                    {
                                        httpHelper.SynchronizeBackground<ReceiptTemplatesEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.STOCKING_ATTRIBUTE_TYPE:
                                    {
                                        httpHelper.SynchronizeBackground<StockingAttributeTypeEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.STOCK_LOCATION:
                                    {
                                        httpHelper.SynchronizeBackground<StockLocationsEntity>(_httpClient);
                                        break;
                                    }
                                case AltoposResourcesEnum.USER_LIST:
                                    {
                                        httpHelper.SynchronizeBackground<UsersEntity>(_httpClient);
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
