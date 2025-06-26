using DevMindSpeed.BusinessLayer.Dtos;
using DevMindSpeed.BusinessLayer.Services.Abstraction;
using DevMindSpeed.DataAccessLayer.Repositories.Abstraction;
using DevMindSpeed.Entity.Aggregates.GameAggregate;
using DevMindSpeed.Entity.Aggregates.QuestionAggregate;
using Mapster;


namespace DevMindSpeed.BusinessLayer.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService (IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDto> CreateQuestion(Game game, int difficulty)
        {

            string equation = NewQuestion.GetEquation(difficulty);
            float correctAnswer = (float)NewQuestion.SolveEquation(equation);

            var question = new Question
            {
                GameId = game.Id,
                Game = game,
                Difficulty = difficulty,
                Equation = equation,
                CorrectAnswer = correctAnswer,
                PlayerAnswer = null,
                TimeAsked = game.TimeStarted,
                TimeTaken = null,
            };

            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();

            

            return question.Adapt<QuestionDto>();

        }
    }
}