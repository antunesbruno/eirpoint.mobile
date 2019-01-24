using Acr.UserDialogs;
using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.DTO;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared.Enumerators;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Api
{
    public class BasicDataApi : IBasicDataApiCore
    {
        #region Fields

        private HttpClient _httpClient;
        private IProgressDialog _progressDialog;
        Action<int> _onProgressCallback;

        #endregion

        public async Task SynchronizeDataItems(Action<int> onProgressCallback, IProgressDialog progressDialog)
        {
            //set local property
            _progressDialog = progressDialog;

            //set callback
            _onProgressCallback = onProgressCallback;

            //declare response
            HttpHelperResponseDTO _httpResponseDTO = new HttpHelperResponseDTO();

            foreach (var endpoint in EntityResourcesHelper.GetValues())
            {
                //complement endpoint (in this case because is using a generic method in refit)
                _httpClient = Endpoints.BaseEirpointHttpClient(endpoint.Value.ToString());

                switch (endpoint.Key)
                {
                    case AltoposResourcesEnum.CUSTOMER:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<CustomersEntity>("Customers...");
                            break;
                        }
                    case AltoposResourcesEnum.DEPARTMENT:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<DepartmentEntity>("Departments...");
                            break;
                        }
                    case AltoposResourcesEnum.DISCOUNT:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<DiscountsEntity>("Discounts...");
                            break;
                        }
                    case AltoposResourcesEnum.PAYMENT_CATEGORY:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<PaymentsCategoriesEntity>("Payments Categories...");
                            break;
                        }
                    case AltoposResourcesEnum.GROUP_LIST:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<GroupsEntity>("Groups...");
                            break;
                        }
                    case AltoposResourcesEnum.POS_STATION:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<PosStationsEntity>("PosStations...");
                            break;
                        }
                    case AltoposResourcesEnum.PRODUCT:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<ProductsEntity>("Products...");
                            break;
                        }
                    case AltoposResourcesEnum.PRODUCT_BARCODE:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<ProductBarCodesEntity>("Products Barcodes...");
                            break;
                        }
                    case AltoposResourcesEnum.PROMOTIONS:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<PromotionsEntity>("Promotions...");
                            break;
                        }
                    case AltoposResourcesEnum.REASON:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<ReasonsEntity>("Reasons...");
                            break;
                        }
                    case AltoposResourcesEnum.RECEIPT:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<ReceiptTemplatesEntity>("Receipt Templates...");
                            break;
                        }
                    case AltoposResourcesEnum.STOCKING_ATTRIBUTE_TYPE:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<StockingAttributeTypeEntity>("Stocking Attributes Types...");
                            break;
                        }
                    case AltoposResourcesEnum.STOCK_LOCATION:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<StockLocationsEntity>("Stocking Locations...");
                            break;
                        }
                    case AltoposResourcesEnum.USER_LIST:
                        {
                            _httpResponseDTO = await ConfigureSynchronism<UsersEntity>("Users...");
                            break;
                        }
                }

                //if error stop cycle and return to view
                if (!string.IsNullOrEmpty(_httpResponseDTO.MessageError))
                {
                    //hide dialog
                    _progressDialog.Hide();

                    //finish cycle
                    break;
                }

                //hide dialog
                _progressDialog.Hide();
            }
        }

        /// <summary>
        /// Configure common informations for sync
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="title"></param>
        /// <param name="httpClient"></param>
        /// <param name="onProgressCallback"></param>
        /// <returns></returns>
        private async Task<HttpHelperResponseDTO> ConfigureSynchronism<E>(string title) where E : EntityBase
        {
            _progressDialog.Title = title;
            _progressDialog.PercentComplete = 0;
            _progressDialog.Show();

            var httpResponseDTO = await new HttpHelper().Synchronize<E>(_httpClient, _onProgressCallback);

            _progressDialog.Hide();

            return httpResponseDTO;
        }
    }
}