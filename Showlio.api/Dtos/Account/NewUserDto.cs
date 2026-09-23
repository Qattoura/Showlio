namespace Showlio.api.Dtos.Account
{
    public class NewUserDto
    {
        public string? UserName { get; set; }
        public Guid? Id { get; set; }
        public string? Token { get; set; }
    }
}
