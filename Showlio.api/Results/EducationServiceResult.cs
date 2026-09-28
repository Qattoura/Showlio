using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class EducationServiceResult<T>
    {
        public EducationServiceStatus Status { get; }

        public T? Data { get; }

        public EducationServiceResult(
            EducationServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}
