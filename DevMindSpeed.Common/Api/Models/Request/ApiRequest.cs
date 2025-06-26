using DevMindSpeed.Common.Api.Models.Request;

namespace DevMindSpeed.Common.Api.Models.Request
{
    public class ApiRequest : IApiRequest
    {
    }

    public class ApiRequest<T> : IApiRequest<T>
    {
        public T Request { get; set; }
    }
}