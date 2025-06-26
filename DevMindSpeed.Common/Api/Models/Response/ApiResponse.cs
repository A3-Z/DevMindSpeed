namespace DevMindSpeed.Common.Api.Models.Response
{
    public class ApiResponse : IApiResponse
    {
        public virtual bool IsSuccess
        {
            get
            {
                return InternalErrors is null || InternalErrors.Any();
            }
            set { }
        }
        public virtual List<InternalError> InternalErrors { get; set; }
    }

    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess
        {
            get
            {
                return InternalErrors is null || !InternalErrors.Any();
            }
            set { }
        }
        public List<InternalError> InternalErrors { get; set; } = new List<InternalError>();

        public ApiResponse() { }

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}