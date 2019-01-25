using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Api
{
    public interface IGenericApi<T, in TKey> where T : class
    {
        [Get("")]
        Task<HttpResponseMessage> GetAll();
    }

    public interface IGenericApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetAll();

        [Get("/{endpoint}")]
        Task<HttpResponseMessage> GetAllLastModified([AliasAs("endpoint")] string endpoint, [AliasAs("LastModified")] string lastModifiedDate);
    }
}
