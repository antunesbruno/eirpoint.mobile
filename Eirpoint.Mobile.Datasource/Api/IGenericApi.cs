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
}
