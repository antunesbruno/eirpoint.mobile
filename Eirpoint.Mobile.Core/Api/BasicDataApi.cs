using Acr.UserDialogs;
using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.DTO;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared.Enumerators;
using System;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Api
{
    public class BasicDataApi : IBasicDataApiCore
    {
        public async Task SynchronizeDataItems(Action<int> onProgressCallback, IProgressDialog _progressDialog)
        {
            //declare response
            HttpHelperResponseDTO _httpResponseDTO = new HttpHelperResponseDTO();

            foreach (var endpoint in AltoposResources.GetValues())
            {
                //complement endpoint (in this case because is using a generic method in refit)
                var httpClient = Endpoints.BaseEirpointHttpClient(endpoint.Value.ToString());

                switch (endpoint.Key)
                {                 
                    case AltoposResourcesEnum.CUSTOMER:
                        {
                            _progressDialog.Title = "Customers...";
                            _progressDialog.PercentComplete = 0;
                            _progressDialog.Show();

                            _httpResponseDTO = await new HttpHelper().Synchronize<CustomersEntity>(httpClient, onProgressCallback);

                            _progressDialog.Hide();

                            break;
                        }
                    case AltoposResourcesEnum.PRODUCT:
                        {
                            _progressDialog.Title = "Products...";
                            _progressDialog.PercentComplete = 0;

                            _progressDialog.Show();

                            _httpResponseDTO = await new HttpHelper().Synchronize<ProductsEntity>(httpClient, onProgressCallback);

                            _progressDialog.Hide();
                            break;
                        }
                    case AltoposResourcesEnum.DEPARTMENT:
                        {
                            _progressDialog.Title = "Departments...";
                            _progressDialog.PercentComplete = 0;
                            _progressDialog.Show();

                            _httpResponseDTO = await new HttpHelper().Synchronize<DepartmentEntity>(httpClient, onProgressCallback);

                            _progressDialog.Hide();
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

            }
        }
    }
}