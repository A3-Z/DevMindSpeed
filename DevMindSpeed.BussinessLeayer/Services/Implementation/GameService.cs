using DevMindSpeed.BusinessLayer.Dtos;
using DevMindSpeed.BusinessLayer.Services.Abstraction;
using DevMindSpeed.DataAccessLayer.Repositories.Abstraction;
using DevMindSpeed.Entity.Aggregates.GameAggregate;
using DevMindSpeed.Entity.Api.Game;
using DevMindSpeed.Entity.Api.Question;
using DevMindSpeed.Common.Api.Models.Response;
using Mapster;
using FarmSystem.Common.Helpers;


namespace DevMindSpeed.BusinessLayer.Services.Implementation
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionService _questionService;

        public GameService (IGameRepository gameRepository, IQuestionRepository questionRepository, IQuestionService questionService)
        {
            _gameRepository = gameRepository;
            _questionRepository = questionRepository;
            _questionService = questionService;
        }

        public async Task<ApiResponse<StartGameResponseDto>> StartGameAsync(StartGameRequest request)
        {
            
            var internalResponse = Validate(request);
            if (!internalResponse.IsSuccess)
            {
                return internalResponse;
            }

            var game = Game.Start(request);
            
            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveChangesAsync();

            var question = await _questionService.CreateQuestion(game, request.Difficulty);


            var response = new StartGameResponseDto
            {
                Message = $"Hello {request.PlayerName}, find your submit API URL below",
                SubmitUrl = $"/game/{game.Id}/submit",
                Question = question.Equation,
                TimeStarted = game.TimeStarted,
                QuestionId = question.Id
            };

            internalResponse.Data = response; 
            return internalResponse;
        }

        public async Task<ApiResponse<SubmitAnswerResponseDto>> SubmitAnswerAsync(Guid gameId, SubmitAnswerRequest request)
        {

            var internalResponse = await Validate(request);
            if (!internalResponse.IsSuccess)
            {
                return internalResponse;
            }
            // Get Game and Question
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game == null || game.Ended)
            {
                internalResponse.InternalErrors.Add(new InternalError { Message = "The Game is already finished, you can start a new game *_* " });
                return internalResponse;
            }
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
           
            // Update Current Questioin Data

            bool isCorrect = IsCorrect(request.PlayerAnswer, question.CorrectAnswer);

            string resultMessage = GetResultMessage(isCorrect, game.PlayerName);

            float timeTaken = TimeTaken(question.TimeAsked); 

            int totalQuestionsAnswered = game.Questions.Count;

            float updatedScore = (game.CurrentScore + (isCorrect ? 1 : 0)) / (totalQuestionsAnswered);

            question.Update(request);
            await _questionRepository.UpdateAsync(question);
            await _questionRepository.SaveChangesAsync();


            // Update Game Data
            float updatedBestScore = timeTaken < game.BestScore ? timeTaken : game.BestScore;

            game.BestScore = updatedBestScore;
            game.CurrentScore = updatedScore;
            game.TotalTimeSpent += timeTaken;
            
            await _gameRepository.UpdateAsync(game);
            await _gameRepository.SaveChangesAsync();

            //Create Next Question
            var nextQuestion = await _questionService.CreateQuestion(game, game.Difficulty);
            
            // Create Response
            var response = new SubmitAnswerResponseDto
            {
                Result = resultMessage,
                TimeTaken = timeTaken,
                CurrentScore = updatedScore,
                NextQuestion = new NextQuestionDto
                {
                    SubmitUrl = $"/game/{game.Id}/submit",
                    Question = nextQuestion.Equation,
                    QuestionId = nextQuestion.Id
                }
            };

            internalResponse.Data = response;
            return internalResponse;
        }

        
        public async Task<ApiResponse<EndGameResponseDto>> EndGameAsync(Guid gameId)
        {
            var internalResponse = new ApiResponse<EndGameResponseDto>();

            // Get game
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game == null || game.Ended)
            {
                internalResponse.InternalErrors.Add(new InternalError
                {
                    Message = "The game is already finished. You can start a new game *_*"
                });
                return internalResponse;
            }

            //  Mark as ended
            game.Ended = true;
            await _gameRepository.UpdateAsync(game);
            await _gameRepository.SaveChangesAsync();

           

            // Get game's question history
            var gameQuestions = await _questionRepository.GetAllQuestionsByGameId(game.Id);
            
            //Best Question (the least time and answered)
            var bestQuestion = gameQuestions.Count > 0 ? gameQuestions[0] : null;

            //checking if there is no questions in  the game
            bool store = (bestQuestion != null);
            
            var Best = new BestScoreDto
            {
                Equation = store? bestQuestion.Equation:"",
                PlayerAnswer = store?(float)bestQuestion.PlayerAnswer:0,
                TimeTaken = store?(float)bestQuestion.TimeTaken:0
            };
           
            // 5. Create response
            var response = new EndGameResponseDto
            {
                PlayerName = game.PlayerName,
                Difficulty = game.Difficulty,
                CurrentScore = game.CurrentScore,
                TotalTimeSpent = (float)(DateTime.Now - game.TimeStarted).TotalSeconds,
                BestScore = Best,
                History = gameQuestions.Adapt<List<QuestionHistoryDto>>()
            };

            internalResponse.Data = response;
            return internalResponse;
        }


        private ApiResponse<StartGameResponseDto> Validate(StartGameRequest request)
        {
            var internalResponse = new ApiResponse<StartGameResponseDto>();
            if(request == null)
            {
                internalResponse.InternalErrors.Add(new InternalError { Message = "Request Body is Empty (Null)" });
            }
            else
            {
                    if (request.PlayerName.IsNullOrWhiteSpace())
                    {
                        internalResponse.InternalErrors.Add(new InternalError { Message = "Please provide your name to start the game ^_^" });
                    }
                    if (request.Difficulty < 0 || request.Difficulty > 4)
                    {
                        internalResponse.InternalErrors.Add(new InternalError { Message = "The Game Difficulty must be 1, 2, 3, or 4" });
                    } 
            }
            return internalResponse;
        }

        private async Task<ApiResponse<SubmitAnswerResponseDto>> Validate(SubmitAnswerRequest request)
        {
            var internalResponse = new ApiResponse<SubmitAnswerResponseDto>();
            if(request == null)
            {
                internalResponse.InternalErrors.Add(new InternalError { Message = "Request Body is Empty (Null)" });
            }
            else
            {
                var question = await _questionRepository.GetByIdAsync(request.QuestionId);
                if (question == null)
                {
                    internalResponse.InternalErrors.Add(new InternalError { Message = "The Question Does Not Exist" });
                }
            }
            return internalResponse;
        }

        private static string GetResultMessage (bool isCorrect, string name)
        {

            string resultMessage = isCorrect
                ? $"Good job {name}, your answer is correct!"
                : $"Sorry {name}, your answer is incorrect.";

            return resultMessage;
        }

        private static bool IsCorrect (float playerAnswer, float correctAnswer)
        {
            float PlayerAnswer = (float)Math.Round(playerAnswer, 2);
            float CorrectAnswer = (float)Math.Round(correctAnswer, 2);
            bool isCorrect = (PlayerAnswer == CorrectAnswer);

            return isCorrect;
        }

        private static float TimeTaken (DateTime timeAsked)
        {
            float timeTaken = (float)(DateTime.Now - timeAsked).TotalSeconds;
            return timeTaken;
        }

    }
}