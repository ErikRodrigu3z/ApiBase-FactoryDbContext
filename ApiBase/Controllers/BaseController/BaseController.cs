using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;
using Repository.BaseRepository;

namespace ApiBase.Controllers.BaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        private readonly IRepository<TEntity> _repo;

        public BaseController(IRepository<TEntity> repo)
        {
            _repo = repo;
        }

        // GET 
        [HttpGet]
        public IEnumerable<TEntity> Get()
        {
            return _repo.GetAll();
        }

        // GET 
        [HttpGet("{id}")]
        public TEntity Get(int id)
        {
            return _repo.GetById(id);
        }

        // POST 
        [HttpPost]
        public bool Post([FromBody] TEntity value)
        {           
            try
            {
                _repo.Add(value);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        // PUT 
        [HttpPut("{id}")]
        public bool Put([FromBody] TEntity value)
        {
            try
            {
                _repo.Update(value);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        // DELETE 
        [HttpDelete("{id}")]
        public bool Delete(TEntity value)
        {
            try
            {
                _repo.Delete(value);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
