using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Npgsql;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<UserController>
    {
        private readonly PhloxContext Context;

        public UserController(PhloxContext context) 
        { 
            this.Context = context;
        }

        [HttpGet]
        public IEnumerable<Users> Get()
        {
            Logger.LogInformation("UserController.Get called");

            return this.Context.users;
        }


    }
}