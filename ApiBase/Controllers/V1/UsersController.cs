using ApiBase.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Auth;
using Persistence;
using Persistence.EF;
using Repository.BaseRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiBase.Controllers.V1
{   
    public class UsersController : BaseController<Test>
    { 
        public UsersController(IRepository<Test> repo) : base(repo) 
        {
            
        }
    }
}
