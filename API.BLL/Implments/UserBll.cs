using Api.DAL;

namespace Api.BLL
{
    public class UserBll : IUserBll
    {
        UserDal userDal = new UserDal();
        public List<User> GetAll()
        {
            return userDal.GetAll();
        }
        public User GetByName(string name)
        {
            return userDal.GetByName(name);
        }
        public void Post(string userName, string password, string name)
        {
            userDal.Post(userName, password, name);
        }
        public void Put(int id, string? userName, string? password, string? name)
        {
            userDal.Put(id, userName, password, name);
        }
        public void Delete(int id)
        {
            userDal.Delete(id);
        }
    }
}
