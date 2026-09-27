using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class SkillServiceResult<T>
    {
        public SkillServiceStatus Status { get; }
        public T? Data { get; }

        public SkillServiceResult(
            SkillServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}
