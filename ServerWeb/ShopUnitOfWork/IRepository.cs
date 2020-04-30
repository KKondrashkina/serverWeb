using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork
{
    public interface IRepository<T> : IRepository where T : class
    {
        IDbContextTransaction BeginTransaction();

        void Add(T entity);

        void AddRange(List<T> entities);

        void Update(T entity);

        void Delete(T entity);

        T[] GetAll();

        T GetById(int id);
    }
}
