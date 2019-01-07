using Eirpoint.Mobile.Datasource.Repository.Entity;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Api
{
    public interface IProductsApi
    {
        [Get("/products")]
        Task<HttpResponseMessage> GetAllProducts();

        [Get("/products/{id}")]
        Task<HttpResponseMessage> GetById(long id);      
    }
}
