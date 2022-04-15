using Microsoft.AspNetCore.Mvc;
using Api.BLL;
using Api.DAL;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("Local")]
    public class SignController : ControllerBase
    {
        private readonly ISignBll SignBll;

        public SignController(ISignBll SignBll)
        {
            this.SignBll = SignBll;
        }

        [HttpGet("{userName}/{password}")]
        public User SignIn(string userName, string password)
        {
            return SignBll.SignIn(userName, password);
        }

        [HttpPost("{userName}/{password}/{name}")]
        public string SignUp(string userName, string password, string name)
        {
            return SignBll.SignUp(userName, password, name);
        }
    }
}
