using Api.DAL;

namespace Api.BLL
{
    public interface IUserBll
    {
        List<User> GetAll();
        User GetByName(string name);
        List<User> GetByUserNameAndName(string? userName, string? name);
        void Post(string userName, string password, string name);
        void Put(int id, string? userName, string? password, string? name);
        void PutByUserName(string userName, string? password, string? name);
        void Delete(int id);
        void DeleteByUserName(string userName);
    }
}