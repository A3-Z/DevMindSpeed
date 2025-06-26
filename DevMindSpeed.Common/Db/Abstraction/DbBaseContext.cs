using DevMindSpeed.Common.Db.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DevMindSpeed.Common.Db.Abstraction
{
    public class DbBaseContext : DbContext, IUnitOfWork, IDbContext
    {
        public DbBaseContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}