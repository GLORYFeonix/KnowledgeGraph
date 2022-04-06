using Microsoft.AspNetCore.Mvc;
using UserApi.BLL;
using UserApi.DAL;

namespace UserApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBll userBll;

        public UsersController(IUserBll userBll)
        {
            this.userBll = userBll;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            List<User> users = userBll.GetAll();
            return users;
        }
        [HttpGet]
        public User GetByName(string name)
        {
            return userBll.GetByName(name);
        }
        [HttpPost]
        public void Post(string userName, string password, string name)
        {
            userBll.Post(userName, password, name);
        }
        [HttpPut]
        public void Put(int id, string? userName, string? password, string? name)
        {
            userBll.Put(id, userName, password, name);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            userBll.Delete(id);
        }
    }
}
