using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Datasource.Repository.Urls;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Api
{
    public class BarcodeProductsApi : IBarcodeProductsApiCore
    {
        /// <summary>
        /// Get barcode product by code
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public async Task<BarcodesEntity> GetBarcodeProductByCode(string barcode)
        {
            try
            {
                var productsAPI = RestService.For<IBarcodeProductsApi>(Endpoints.BaseEirpointHttpClient());
                var productsResponse = await productsAPI.GetByCode(barcode);

                if (productsResponse.IsSuccessStatusCode)
                {
                    var response = await productsResponse.Content.ReadAsStringAsync();
                    var json = await Task.Run(() => JsonConvert.DeserializeObject<List<BarcodesEntity>>(response));
                    return json.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new BarcodesEntity();
        }
    }
}
