using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class ExperienceServiceResult<T>
    {
        public ExperienceServiceStatus Status { get; }
        public T? Data { get; }

        public ExperienceServiceResult(
            ExperienceServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}