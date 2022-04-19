namespace Api.DAL
{
    public interface IUserDal
    {
        public List<User> GetAll();
        public User GetByName(string name);
        // public User GetByUserNameAndName(string? userName, string? name);
        public void Post(string userName, string password, string name);
        public void Put(int id, string? userName, string? password, string? name);
        public void PutByUserName(string userName, string? password, string? name);
        public void Delete(int id);
        public void DeleteByUserName(string userName);
    }
}