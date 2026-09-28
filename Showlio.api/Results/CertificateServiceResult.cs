using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class CertificateServiceResult<T>
    {
        public CertificateServiceStatus Status { get; }
        public T? Data { get; }

        public CertificateServiceResult(
            CertificateServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}