using Api.DAL;

namespace Api.BLL
{
    public interface ISignBll
    {
        User SignIn(string userName, string password);
        string SignUp(string userName, string password, string name);
    }
}