using Eirpoint.Mobile.Datasource.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Interfaces
{
    public interface IBarcodeProductsApiCore
    {
        Task<BarcodesEntity> GetBarcodeProductByCode(string barcode);
    }
}
