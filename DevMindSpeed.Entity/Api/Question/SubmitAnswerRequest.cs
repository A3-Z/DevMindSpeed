
namespace DevMindSpeed.Entity.Api.Question
{
    public class SubmitAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public float PlayerAnswer {  get; set; }

    }
}
