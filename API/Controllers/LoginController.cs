using Microsoft.AspNetCore.Mvc;
using Api.BLL;
using Api.DAL;

namespace UserApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
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
