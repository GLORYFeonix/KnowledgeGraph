using Api.DAL;

namespace Api.BLL
{
    public class LoginBll : ILoginBll
    {
        UserDal userDal = new UserDal();

        public User Login(string userName, string password)
        {
            var user = userDal.GetByUserName(userName);
            return user.Password == password ? user : default;
        }
    }
}
