using DevMindSpeed.BusinessLayer.Dtos;
using DevMindSpeed.Entity.Aggregates.GameAggregate;


namespace DevMindSpeed.BusinessLayer.Services.Abstraction
{
    public interface IQuestionService 
    {
        Task<QuestionDto> CreateQuestion(Game game, int difficulty);
    }
}  
