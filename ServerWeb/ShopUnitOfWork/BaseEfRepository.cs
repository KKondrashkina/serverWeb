using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork
{
    public abstract class BaseEfRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Db;
        protected DbSet<T> DbSet;

        protected BaseEfRepository(DbContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public virtual void Delete(T entity)
        {
            if (Db.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }

        public virtual T[] GetAll()
        {
            return DbSet.ToArray();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void AddRange(List<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            Db.Entry(entity).State = EntityState.Modified;
        }
    }
}
