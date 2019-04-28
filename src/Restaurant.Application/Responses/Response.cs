using Newtonsoft.Json;

namespace Restaurant.Application.Responses
{
    public class Response<T> where T : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Value { get; }

        public string Error { get; }

        public Response(T value, string error)
        {
            Value = value;
            Error = error;
        }
    }
}
