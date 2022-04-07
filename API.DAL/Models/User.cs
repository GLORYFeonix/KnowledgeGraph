using Microsoft.EntityFrameworkCore;

namespace UserApi.DAL
{
    [Index(nameof(UserName), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
