using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        public T GetById(int? id);
        void Add(T item);
        void Update(T item);
        void Delete(int? id);
    }
}
