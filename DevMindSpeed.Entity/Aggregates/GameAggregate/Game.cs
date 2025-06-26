using DevMindSpeed.Common.Core.Abstractions;
using DevMindSpeed.Entity.Aggregates.QuestionAggregate;
using DevMindSpeed.Entity.Api.Game;

namespace DevMindSpeed.Entity.Aggregates.GameAggregate
{
    public class Game : BaseEntity<Guid>
    {
        public string? PlayerName { get; set; }
        public int Difficulty { get; set; }
        public DateTime TimeStarted { get; set; }
        public float CurrentScore { get; set; }
        public float? TotalTimeSpent { get; set; }
        public bool Ended { get; set; }
        public float BestScore { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public static Game Start(StartGameRequest startGameRequest)
        {
            var game = new Game();

            game.PlayerName = startGameRequest.PlayerName;
            game.Difficulty = startGameRequest.Difficulty;
            game.TimeStarted = DateTime.Now;
            game.CurrentScore = 0;
            game.TotalTimeSpent = 0;
            game.Ended = false;
            game.BestScore = 100000;

       
            return game;
        }
    }
}
