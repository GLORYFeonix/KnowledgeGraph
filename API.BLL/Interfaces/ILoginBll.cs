using Api.DAL;

namespace Api.BLL
{
    public interface ILoginBll
    {
        User Login(string userName, string password);
    }
}