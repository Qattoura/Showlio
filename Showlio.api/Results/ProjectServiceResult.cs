using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class ProjectServiceResult<T>
    {
        public ProjectServiceStatus Status { get; }
        public T? Data { get; }

        public ProjectServiceResult(
            ProjectServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}
