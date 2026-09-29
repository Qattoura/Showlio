namespace Showlio.api.Interfaces.IServices
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Role { get; }

    }
}
