
namespace DevMindSpeed.BusinessLayer.Dtos
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public int Difficulty { get; set; }
        public string? Equation { get; set; }
        public float CorrectAnswer { get; set; }
        public float? PlayerAnswer { get; set; }
        public DateTime TimeAsked { get; set; }
        public float? TimeTaken { get; set; }

        
    }
}
