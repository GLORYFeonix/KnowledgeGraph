﻿using Microsoft.AspNetCore.Mvc;
using Api.BLL;
using Api.DAL;

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
        [HttpDelete]
        public void DeleteUser(int id)
        {
            userBll.Delete(id);
        }
    }
}