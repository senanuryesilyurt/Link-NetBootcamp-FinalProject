using Entities.Abstract;
using System.Linq.Expressions;

namespace Core.Data.EntityFramework
{
    public interface IGenericRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll();

        //Expression filtreleme işlemi yapmamızı sağlar.
        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
