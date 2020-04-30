using System;
using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.RepositoryClasses;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext db;
        public UnitOfWork(DbContext db)
        {
            this.db = db;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            if (typeof(T) == typeof(ICategoryRepository))
            {
                return new CategoryRepository(db) as T;
            }

            if (typeof(T) == typeof(ICustomerRepository))
            {
                return new CustomerRepository(db) as T;
            }

            if (typeof(T) == typeof(IOrderRepository))
            {
                return new OrderRepository(db) as T;
            }

            if (typeof(T) == typeof(IProductRepository))
            {
                return new ProductRepository(db) as T;
            }

            throw new Exception("Неизвестный тип репозитория: " + typeof(T));
        }
    }
}
