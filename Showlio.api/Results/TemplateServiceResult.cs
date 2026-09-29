using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class TemplateServiceResult<T>
    {
        public TemplateServiceStatus Status { get; set; }
        public T? Data { get; set; }

        public TemplateServiceResult(
            TemplateServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}
