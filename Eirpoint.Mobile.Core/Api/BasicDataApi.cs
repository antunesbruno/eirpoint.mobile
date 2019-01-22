using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Platform.Ioc.Injection;
using Eirpoint.Mobile.Datasource.Repository.Base;
using Eirpoint.Mobile.Shared.Enumerators;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using System.Reflection;

namespace Eirpoint.Mobile.Core.Api
{
    public class BasicDataApi : IBasicDataApiCore
    {
        public async Task SynchronizeDataItems(Action<int> onProgressCallback)
        {
            foreach (var endpoint in AltoposResources.GetValues())
            {
                //complement endpoint (in this case because is using a generic method in refit)
                var httpClient = Endpoints.BaseEirpointHttpClient(endpoint.Value.ToString());

                switch (endpoint.Key)
                {
                    case AltoposResourcesEnum.PRODUCT:
                        {
                            await new HttpHelper().Synchronize<ProductsEntity>(httpClient, onProgressCallback);
                            break;
                        }
                    case AltoposResourcesEnum.DEPARTMENT:
                        {
                            await new HttpHelper().Synchronize<DepartmentEntity>(httpClient, onProgressCallback);
                            break;
                        }
                }               
            }
        }
    }
}