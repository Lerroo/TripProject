using FastTripApp.DAO.Repository.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryGeneric<T> : IRepository<T> where T : class
    {
        private readonly UsingIdentityContext _сontext;
        private readonly DbSet<T> _dbSet;

        public RepositoryGeneric(UsingIdentityContext usingIdentityContext)
        {
            _сontext = usingIdentityContext;
            _dbSet = _сontext.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public T GetById(int? id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.AddAsync(item);
            _сontext.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = _dbSet.Find(id);

            if (_сontext.Entry(item).State == EntityState.Detached)
            {
                _dbSet.Attach(item);
            }

            _dbSet.Remove(item);
            _сontext.SaveChanges();
        }

        public void Update(T item)
        {
            if (_сontext.Entry(item).State == EntityState.Detached)
            {
                _dbSet.Attach(item);
            }

            _сontext.Entry(item).State = EntityState.Modified;
            _сontext.SaveChanges();
        }
    }
}
