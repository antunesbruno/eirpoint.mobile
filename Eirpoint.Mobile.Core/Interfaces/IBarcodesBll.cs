using Eirpoint.Mobile.Datasource.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Interfaces
{
    public interface IBarcodesBll
    {
        Task<BarcodesEntity> GetProductByBarcode(string barcodeData);
    }
}
