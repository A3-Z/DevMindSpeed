using DevMindSpeed.Common.Db.Abstraction;
using DevMindSpeed.Entity.Aggregates.GameAggregate;
using DevMindSpeed.Common.Db.Models;
using DevMindSpeed.DataAccessLayer.Repositories.Abstraction;

namespace DevMindSpeed.DataAccessLayer.Repositories.Implementation
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext, RequestUserEntity requestUserEntity)
            : base(dbContext, requestUserEntity)
        {
        }
    }
}
