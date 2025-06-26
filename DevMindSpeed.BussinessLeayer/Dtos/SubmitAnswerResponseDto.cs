

namespace DevMindSpeed.BusinessLayer.Dtos
{
    public class SubmitAnswerResponseDto
    {
        public string? Result { get; set; }
        public float TimeTaken { get; set; }
        public NextQuestionDto? NextQuestion { get; set; }
        public float CurrentScore { get; set; }
    }

    public class NextQuestionDto
    {
        public string? SubmitUrl { get; set; }
        public string? Question { get; set; }
        public Guid QuestionId { get; set; }
    }

}
