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
        public List<User> GetByUserNameAndName(string? userName, string? name)
        {
            // return userDal.GetByUserNameAndName(userName, name);
            if (userName == null && name == null)
            {
                return userDal.GetAll();
            }
            else if (userName != null && name == null)
            {
                List<User> users = new();
                users.Add(userDal.GetByUserName(userName));
                return users;
            }
            else if (userName == null && name != null)
            {
                List<User> users = new();
                users.Add(userDal.GetByName(name));
                return users;
            }
            else
            {
                List<User> users = new();
                users.Add(userDal.GetByUserName(userName));
                return users;
            }
        }
        public void Post(string userName, string password, string name)
        {
            userDal.Post(userName, password, name);
        }
        public void Put(int id, string? userName, string? password, string? name)
        {
            userDal.Put(id, userName, password, name);
        }
        public void PutByUserName(string userName, string? password, string? name)
        {
            userDal.PutByUserName(userName, password, name);
        }
        public void Delete(int id)
        {
            userDal.Delete(id);
        }
        public void DeleteByUserName(string userName)
        {
            userDal.DeleteByUserName(userName);
        }
    }
}
