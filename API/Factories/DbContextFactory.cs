using Microsoft.EntityFrameworkCore;
using Api.DAL;

namespace UserApi.Factories
{
    public class DbContextFactory
    {
        private static UserContext _dbContext = null;
        private DbContextFactory()
        {

        }
        public static UserContext GetDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new UserContext();
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            return _dbContext;
        }
    }
}
