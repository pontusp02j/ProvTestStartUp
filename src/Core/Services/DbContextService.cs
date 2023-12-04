using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class DbContextService : DbContext
    {

        public DbContextService(DbContextOptions<DbContextService> options) : base(options)
        {

        }
    }
}