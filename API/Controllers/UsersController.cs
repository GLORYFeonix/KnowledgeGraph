using Microsoft.AspNetCore.Mvc;
using Api.BLL;
using Api.DAL;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("Local")]
    public class UsersController : ControllerBase
    {
        private readonly IUserBll userBll;

        public UsersController(IUserBll userBll)
        {
            this.userBll = userBll;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = userBll.GetAll();
            return users;
        }
        [HttpGet]
        public User GetUserByName(string name)
        {
            return userBll.GetByName(name);
        }
        [HttpGet]
        public List<User> GetUserByUserNameAndName(string? userName, string? name)
        {
            return userBll.GetByUserNameAndName(userName, name);
        }
        [HttpPost]
        public void CreateUser(string userName, string password, string name)
        {
            userBll.Post(userName, password, name);
        }
        [HttpPut]
        public void UpdataUser(int id, string? userName, string? password, string? name)
        {
            userBll.Put(id, userName, password, name);
        }
        [HttpPut]
        public void UpdataUserByUserName(string userName, string? password, string? name)
        {
            userBll.PutByUserName(userName, password, name);
        }
        [HttpDelete]
        public void DeleteUser(int id)
        {
            userBll.Delete(id);
        }
        [HttpDelete]
        public void DeleteUserByUserName(string userName)
        {
            userBll.DeleteByUserName(userName);
        }
    }
}
