using DevMindSpeed.Common.Db.Abstraction;
using DevMindSpeed.Entity.Aggregates.QuestionAggregate;

namespace DevMindSpeed.DataAccessLayer.Repositories.Abstraction
{  
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<List<Question>> GetAllQuestionsByGameId(Guid gameId);
    }
}
