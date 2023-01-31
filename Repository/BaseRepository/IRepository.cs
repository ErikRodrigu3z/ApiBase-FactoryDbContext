using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BaseRepository
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T GetById(string id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
