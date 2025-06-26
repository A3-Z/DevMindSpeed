using DevMindSpeed.Common.Db.Abstraction;
using DevMindSpeed.Common.Db.Models;
using DevMindSpeed.DataAccessLayer.Repositories.Abstraction;
using DevMindSpeed.Entity.Aggregates.QuestionAggregate;
using Microsoft.EntityFrameworkCore;

namespace DevMindSpeed.DataAccessLayer.Repositories.Implementation
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext, RequestUserEntity requestUserEntity)
            : base(dbContext, requestUserEntity)
        {
        }

        public async Task<List<Question>> GetAllQuestionsByGameId(Guid gameId)
        {
            
            return await DbSet
                .Where(q => q.GameId == gameId && q.PlayerAnswer != null)
                .OrderBy(q => q.TimeTaken)
                .ToListAsync(); 

        }
    }
}
