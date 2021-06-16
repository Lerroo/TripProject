using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        public T GetById(int? id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
