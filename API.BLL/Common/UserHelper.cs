using Api.DAL;

namespace Api.BLL
{
    public static class UserHelper
    {
        public static User ToUser(int? id, string? userName, string? password, string? name)
        {
            User user = new();
            user.id = id ?? user.id;
            user.UserName = userName ?? user.UserName;
            user.Password = password ?? user.Password;
            user.Name = name ?? user.Name;
            return user;
        }
    }
}