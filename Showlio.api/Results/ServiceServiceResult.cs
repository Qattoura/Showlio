using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class ServiceServiceResult<T>
    {
        public ServiceServiceStatus Status { get; }

        public T? Data { get; }

        public ServiceServiceResult(
            ServiceServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}
