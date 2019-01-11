using Eirpoint.Mobile.Datasource.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Interfaces
{
    public interface IProductsApiCore
    {
        Task<ProductsEntity> GetProductById(long id);
        Task<List<ProductsEntity>> GetProductsByPaging(Action<int> onProgressCallback);
    }
}
