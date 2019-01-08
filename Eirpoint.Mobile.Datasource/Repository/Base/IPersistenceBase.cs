using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Repository.Base
{  
    public interface IPersistenceBase<T> where T : class
    {
        Task<List<T>> Get();

        Task<T> Get(int id);

        Task<ObservableCollection<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);

        Task<T> Get(Expression<Func<T, bool>> predicate);

        Task<int> Insert(T entity);

        Task<int> InsertAll(IEnumerable<T> entity);

        Task<int> Update(T entity);

        Task<int> Delete(T entity);

        Task DeleteAll(IEnumerable<T> entity);

        Task<int> Count(Expression<Func<T, bool>> predicate = null);
    }
}
