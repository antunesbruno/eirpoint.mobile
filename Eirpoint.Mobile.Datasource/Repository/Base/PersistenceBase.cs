using Eirpoint.Mobile.Datasource.Interfaces;
using Platform.Ioc.Injection;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Repository.Base
{
    public class PersistenceBase<T> : IPersistenceBase<T> where T : class, new()
    {
        private SQLiteAsyncConnection _db;

        public PersistenceBase()
        {
            this._db = PlatformDatabase.SQLiteConnection;
        }       

        public async Task<int> Count(Expression<Func<T, bool>> predicate = null)
        {
            var query = _db.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }

        public async Task<int> Delete(T entity)
        {
            return await _db.DeleteAsync(entity);
        }

        public async Task DeleteAll(IEnumerable<T> entity)
        {
            foreach (var item in entity)
                await _db.DeleteAsync(item);
        }

        public async Task<List<T>> Get()
        {
            return await _db.Table<T>().ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _db.FindAsync<T>(predicate);
        }

        public async Task<T> Get(int id)
        {
            return await _db.FindAsync<T>(id);
        }

        public async Task<ObservableCollection<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _db.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = query.OrderBy<TValue>(orderBy);
            }

            var collection = new ObservableCollection<T>();
            var items = await query.ToListAsync();
            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        public async Task<int> Insert(T entity)
        {
            return await _db.InsertAsync(entity);
        }

        public async Task<int> InsertAll(IEnumerable<T> entity)
        {
            return await _db.InsertAllAsync(entity);
        }

        public async Task<int> Update(T entity)
        {
            return await _db.UpdateAsync(entity);
        }
    }
}
