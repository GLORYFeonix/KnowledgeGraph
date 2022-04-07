using Microsoft.AspNetCore.Mvc;
using Api.BLL;
using Api.DAL;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("Local")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBll loginBll;

        public LoginController(ILoginBll loginBll)
        {
            this.loginBll = loginBll;
        }

        [HttpGet("{userName}/{password}")]
        public User Login(string userName, string password)
        {
            return loginBll.Login(userName, password);
        }
    }
}
