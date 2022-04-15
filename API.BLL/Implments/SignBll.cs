using Api.DAL;

namespace Api.BLL
{
    public class SignBll : ISignBll
    {
        UserDal userDal = new UserDal();

        public User SignIn(string userName, string password)
        {
            var user = userDal.GetByUserName(userName);
            if (user == null)
            {
                return default;
            }
            else
            {
                return user.Password == password ? user : default;
            }
        }
        public string SignUp(string userName, string password, string name)
        {
            var user = userDal.GetByUserName(userName);
            if (user == null)
            {
                user = userDal.GetByName(name);
                if (user == null)
                {
                    userDal.Post(userName, password, name);
                    return "success";
                }
                else
                {
                    return "name";
                }
            }
            else
            {
                return "username";
            }
        }
    }
}
