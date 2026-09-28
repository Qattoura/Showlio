using Showlio.api.Enums;

namespace Showlio.api.Results
{
    public class ContactItemServiceResult<T>
    {
        public ContactItemServiceStatus Status { get; }

        public T? Data { get; }

        public ContactItemServiceResult(
            ContactItemServiceStatus status,
            T? data = default)
        {
            Status = status;
            Data = data;
        }
    }
}
