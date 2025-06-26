using DevMindSpeed.Entity.Aggregates.GameAggregate;
using DevMindSpeed.Entity.Api.Question;
using DevMindSpeed.Common.Core.Abstractions;

namespace DevMindSpeed.Entity.Aggregates.QuestionAggregate
{
    public class Question : BaseEntity<Guid>
    {
        
        public int Difficulty {  get; set; }
        public string Equation { get; set; }
        public float CorrectAnswer { get; set; }
        public float? PlayerAnswer { get; set; }
        public DateTime TimeAsked { get; set; }
        public float? TimeTaken { get; set; } 

        public Guid GameId { get; set; }
        public Game Game { get; set; }
        
        public static Question New (CreateQuestionRequest createQuestionRequest)
        {
            var question = new Question();

            question.GameId = createQuestionRequest.GameId;
            question.Difficulty = createQuestionRequest.Difficulty;
            question.Equation = NewQuestion.GetEquation(question.Difficulty); 
            question.CorrectAnswer = (float)NewQuestion.SolveEquation(question.Equation);
            question.PlayerAnswer = null;
            question.TimeAsked = DateTime.Now;
            question.TimeTaken = null;
           
            return question;
        }
        public void Update(SubmitAnswerRequest submitAnswerRequest)
        {
            this.PlayerAnswer = submitAnswerRequest.PlayerAnswer;
            this.TimeTaken =(float) (DateTime.Now - TimeAsked).TotalSeconds;
        }
    }
}
