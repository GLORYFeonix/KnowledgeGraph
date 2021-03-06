namespace Api.DAL
{
    public class UserDal : IUserDal
    {
        public List<User> GetAll()
        {
            using UserContext context = new();
            return context.Users.ToList();
        }
        public User GetByName(string name)
        {
            using UserContext context = new();
            return context.Users.FirstOrDefault(x => x.Name == name);
        }
        public User GetByUserName(string userName)
        {
            using UserContext context = new();
            return context.Users.FirstOrDefault(x => x.UserName == userName);
        }
        // public User GetByUserNameAndName(string? userName, string? name)
        // {
        //     using UserContext context = new();
        //     return context.Users.FirstOrDefault(x => x.UserName == userName);
        // }
        public void Post(string userName, string password, string name)
        {
            using UserContext context = new();
            context.Users.Add(new User
            {
                UserName = userName,
                Password = password,
                Name = name,
                IsAdmin = false
            });
            context.SaveChanges();
        }
        public void Put(int id, string? userName, string? password, string? name)
        {
            using UserContext context = new();
            var user = context.Users.FirstOrDefault(m => m.id == id);
            user.UserName = userName ?? user.UserName;
            user.Password = password ?? user.Password;
            user.Name = user.Name;
            context.SaveChanges();
        }
        public void PutByUserName(string userName, string? password, string? name)
        {
            using UserContext context = new();
            var user = context.Users.FirstOrDefault(m => m.UserName == userName);
            user.Password = password ?? user.Password;
            user.Name = name ?? user.Name;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            using UserContext context = new();
            var user = context.Users.FirstOrDefault(m => m.id == id);
            context.Users.Remove(user);
            context.SaveChanges();
        }
        public void DeleteByUserName(string userName)
        {
            using UserContext context = new();
            var user = context.Users.FirstOrDefault(m => m.UserName == userName);
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
