using System;
using Microsoft.EntityFrameworkCore.Storage;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.Uow
{
    interface IUnitOfWork : IDisposable
    {
        void Save();

        T GetRepository<T>() where T : class, IRepository;

        void BeginTransaction();

        void RollbackTransaction();
    }
}
