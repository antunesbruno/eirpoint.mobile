using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Repository.Base;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Platform.Ioc.Injection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Bll
{
    public class BarcodesBll : IBarcodesBll
    {
        public async Task<BarcodesEntity> GetProductByBarcode(string barcodeData)
        {
            var response = await Injector.Resolver<IPersistenceBase<ProductBarCodesEntity>>().Get(x => x.Barcode.Equals(barcodeData));

            if (response != null)
            {
                var barcode = new BarcodesEntity();

                barcode.Id = response.ProductId.HasValue ? response.ProductId.Value : 0;
                barcode.Barcode = response.Barcode;
                barcode.ProductId = response.ProductId;
                barcode.StockItemId = response.StockItemId.HasValue ? response.StockItemId.Value : 0;
                barcode.LastModified = response.LastModified;
                barcode.SyncInsertTimestamp = response.SyncInsertTimestamp;
                barcode.SyncUpdateTimestamp = response.SyncUpdateTimestamp;

                return barcode;
            }

            return null;
        }
    }
}
