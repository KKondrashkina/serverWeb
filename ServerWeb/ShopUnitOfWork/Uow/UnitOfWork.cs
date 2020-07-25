using System;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopUnitOfWork.RepositoryClasses;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;

        private bool _isTransactionExist;

        public UnitOfWork(DbContext db)
        {
            _db = db;
        }

        public virtual void BeginTransaction()
        {
            _isTransactionExist = true;
            _db.Database.BeginTransaction();
        }

        public virtual void RollbackTransaction()
        {
            _isTransactionExist = false;
            _db.Database.RollbackTransaction();
        }

        public void Save()
        {
            if (_isTransactionExist)
            {
                _db.Database.CommitTransaction();
            }

            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_isTransactionExist)
            {
                RollbackTransaction();
            }

            _db.Dispose();
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            if (typeof(T) == typeof(ICategoryRepository))
            {
                return new CategoryRepository(_db) as T;
            }

            if (typeof(T) == typeof(ICustomerRepository))
            {
                return new CustomerRepository(_db) as T;
            }

            if (typeof(T) == typeof(IOrderRepository))
            {
                return new OrderRepository(_db) as T;
            }

            if (typeof(T) == typeof(IProductRepository))
            {
                return new ProductRepository(_db) as T;
            }

            throw new Exception("Неизвестный тип репозитория: " + typeof(T));
        }
    }
}