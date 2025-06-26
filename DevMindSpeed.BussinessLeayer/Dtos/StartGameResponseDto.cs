
namespace DevMindSpeed.BusinessLayer.Dtos
{
    public class StartGameResponseDto
    {
        public string? Message { get; set; }
        public string? SubmitUrl { get; set; }
        public string? Question { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime TimeStarted { get; set; }
    }
}
