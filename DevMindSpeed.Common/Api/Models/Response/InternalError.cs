namespace DevMindSpeed.Common.Api.Models.Response
{
    public class InternalError
    {
        public string Message { get; set; }

        public static InternalError New(string message)
        {
            var error = new InternalError();

            error.Message = message;

            return error;
        }
    }
}