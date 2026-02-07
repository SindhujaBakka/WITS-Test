namespace Models.Dtos
{
    public class UpsertUsernameRequestDto
    {
        public Guid? AccountId { get; set; }
        public string Username { get; set; }
    }
}
