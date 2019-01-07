using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Api
{
    public interface IGenericApi<T, in TKey> where T : class
    {
        [Get("")]
        Task<HttpResponseMessage> GetAll();
    }
}
