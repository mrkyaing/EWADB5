using System.Linq.Expressions;

namespace CloudHRMS.Repositories.Common
{
    public interface IBaseRepository<T> where T : class
    {    //Crud process
        void Create(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Getby(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);
    }
}
