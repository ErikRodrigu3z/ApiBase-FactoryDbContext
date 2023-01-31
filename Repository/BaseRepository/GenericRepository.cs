using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.EF;
using Persistence.MySQL;

namespace Repository.BaseRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly MSSQL_DbContext MSSQL_context;
        private readonly MySQL_DbContext MySQL_context;

        public GenericRepository()
        {
            MSSQL_context = new DbContextFactory("Data Source=DESKTOP-07MSP3L\\SQLEXPRESS;Initial Catalog=BaseApi_Test;User Id=sa;Password=Root1234;TrustServerCertificate=True").Create_EF_DbContext();
            MySQL_context = new DbContextFactory("Server=127.0.0.1; User ID=root; Password=Admin1234; Database=BaseApi_Test").Create_MySQL_DbContext();

        }

        public T GetById(int id)
        {
            var res = MySQL_context.Set<T>().Find(id);
            return MSSQL_context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            var res = MSSQL_context.Set<T>().ToList();
            return MySQL_context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            MSSQL_context.Set<T>().Add(entity);
            MSSQL_context.SaveChanges();
        }

        public void Update(T entity)
        {
            MSSQL_context.Entry(entity).State = EntityState.Modified;
            MSSQL_context.SaveChanges();
        }

        public void Delete(T entity)
        {
            MSSQL_context.Set<T>().Remove(entity);
            MSSQL_context.SaveChanges();
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
