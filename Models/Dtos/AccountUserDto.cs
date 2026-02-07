namespace Models.Dtos
{
    public class AccountUserDto
    {
        public Guid AccountId { get; set; }

        public string Username { get; set; } = default!;
    }
}
