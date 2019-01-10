using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Api
{
    public interface IBarcodeProductsApi
    {
        [Get("/productbarcodes?Barcode={code}")]
        Task<HttpResponseMessage> GetByCode(string code);
    }
}
