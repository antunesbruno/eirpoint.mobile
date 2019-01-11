using Refit;
using System.Net.Http;
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
