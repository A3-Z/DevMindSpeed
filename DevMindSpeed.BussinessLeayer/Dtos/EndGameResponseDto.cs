
namespace DevMindSpeed.BusinessLayer.Dtos
{
    public class EndGameResponseDto
    {
        public string? PlayerName { get; set; }
        public int Difficulty { get; set; }
        public float? CurrentScore { get; set; }
        public float TotalTimeSpent { get; set; }
        public BestScoreDto? BestScore { get; set; }
        public List<QuestionHistoryDto>? History { get; set; }
    }

    public class BestScoreDto
    {
        public string? Equation { get; set; }
        public float PlayerAnswer { get; set; } 
        public float TimeTaken { get; set; }
    }

    public class QuestionHistoryDto
    {
        public string? Equation { get; set; } 
        public float? PlayerAnswer { get; set; }
        public float? TimeTaken { get; set; }
    }

}
