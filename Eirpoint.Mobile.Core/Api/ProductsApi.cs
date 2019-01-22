using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Helpers;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Api
{
    public class ProductsApi : IProductsApiCore
    {
        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductsEntity> GetProductById(long id)
        {
            var productsAPI = RestService.For<IProductsApi>(Endpoints.BaseEirpointHttpClient());
            var productsResponse = await productsAPI.GetById(id);

            if (productsResponse.IsSuccessStatusCode)
            {
                var response = await productsResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<ProductsEntity>(response));
                return json;
            }

            return new ProductsEntity();
        }

        public async Task<List<ProductsEntity>> GetProductsByPaging(Action<int> onProgressCallback)
        {
            //complement endpoint (in this case because is using a generic method in refit)
            var httpClient = Endpoints.BaseEirpointHttpClient("/products");

            //request products
            //var productsRequest = await new HttpHelper().SynchronizeAndGetList<ProductsEntity>(httpClient, onProgressCallback);

            await new HttpHelper().Synchronize<ProductsEntity>(httpClient, onProgressCallback);

            //return products
            //return productsRequest;
            return new List<ProductsEntity>();
        }  
    }
}