using Eirpoint.Mobile.Core.Interfaces;
using Eirpoint.Mobile.Datasource.Repository.Base;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Platform.Ioc.Injection;
using System.Collections.Generic;

namespace Eirpoint.Mobile.Core.Bll
{
    public class ProductsBll : IProductsBll
    {
        /// <summary>
        /// Insert all products in database
        /// </summary>
        /// <param name="productsList"></param>
        public void InsertAllProducts(List<ProductsEntity> productsList)
        {
            var response = Injector.Resolver<IPersistenceBase<ProductsEntity>>().InsertAll(productsList);
        }

        /// <summary>
        /// Insert a single product
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(ProductsEntity entity)
        {
            var response = Injector.Resolver<IPersistenceBase<ProductsEntity>>().Insert(entity);
        }

        /// <summary>
        /// Get products by barcode data
        /// </summary>
        /// <param name="barcodeData"></param>
        /// <returns></returns>
        public ProductsEntity GetProductByBarcode(string barcodeData)
        {
            //var response = Injector.Resolver<IPersistenceBase<ProductsEntity>>().Get(data);

            return new ProductsEntity();
        }
    }
}
