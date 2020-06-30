using System.Collections.Generic;
namespace ShopUnitOfWork.RepositoryInterfaces
{
    public interface IRepository
    {

    }

    public interface IRepository<T> : IRepository where T : class
    {
        void Add(T entity);

        void AddRange(List<T> entities);

        void Update(T entity);

        void Delete(T entity);

        T[] GetAll();

        T GetById(int id);
    }
}
