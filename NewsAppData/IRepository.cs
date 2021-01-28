using NewsAPI.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppData
{
    public interface IRepository 
    {
        Task<List<T>> SelectAll<T>() where T : class;
        Task<List<T>> Find<T>(Expression<Func<T, bool>> searchTermPredicate) where T : class;
        Task<T> SelectById<T>(long id) where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task<List<T>> GetAllAsync<T>(PageParameters pageParamers, Expression<Func<T, bool>> searchTermPredicate, Expression<Func<T, object>> orderByPredicate) where T : class;
    }
}
