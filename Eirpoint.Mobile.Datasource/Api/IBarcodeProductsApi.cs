using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Api
{
    public interface IBarcodeProductsApi
    {
        [Get("/productbarcodes?Barcode={code}")]
        Task<HttpResponseMessage> GetByCode(string code);
    }
}
