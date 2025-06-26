
namespace DevMindSpeed.Entity.Api.Question
{
    public class CreateQuestionRequest
    {
        public  Guid GameId { get; set; }
        public int Difficulty {  get; set; }
    }
}
