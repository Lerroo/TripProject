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

        /// Method to get a list of data from the repository.
        /// </summary>
        /// <returns>
        /// Returns all entities with a specified type from the repository.
        /// </returns>
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        /// <summary>
        /// Method to get one entity from the repository.
        /// </summary>
        /// <param name="id">
        /// Special identifier for an entity in the repository.
        /// </param>
        /// <returns>
        /// Returns entity with a specified type by ID from the repository.
        /// </returns>
        public T GetById(int? id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Method to add a new entity with a specified type in the repository.
        /// </summary>
        /// <param name="item">
        /// Object of a specific type.
        /// </param>
        public void Add(T item)
        {
            _dbSet.AddAsync(item);
            _сontext.SaveChanges();
        }

        /// <summary>
        /// Method for remove an entity of a particular type in the repository.
        /// </summary>
        /// <param name="item">
        /// The required entity object from the repository.
        /// </param>
        public void Delete(T item)
        {
            if (_сontext.Entry(item).State == EntityState.Detached)
            {
                _dbSet.Attach(item);
            }

            _dbSet.Remove(item);
            _сontext.SaveChanges();
        }

        /// <summary>
        /// Method for making changes to an existing entity of a specific type in the repository.
        /// </summary>
        /// <param name="item">
        /// The required entity object from the repository.
        /// </param>
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
