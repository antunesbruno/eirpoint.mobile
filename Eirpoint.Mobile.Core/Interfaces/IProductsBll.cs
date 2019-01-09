using Eirpoint.Mobile.Datasource.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Core.Interfaces
{
    public interface IProductsBll
    {
        void InsertAllProducts(List<ProductsEntity> productsList);
        void Insert(ProductsEntity entity);
        ProductsEntity GetProductByBarcode(string barcodeData);
    }
}
