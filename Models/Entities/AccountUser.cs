using System.ComponentModel.DataAnnotations;
namespace Models.Entities
{
    public class AccountUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AccountId { get; private set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Username { get; private set; } = default!;

        public string UsernameNormalized { get; private set; } = default!;

        public DateTimeOffset CreateDateTime { get; private set; }

        public DateTimeOffset UpdatedDateTime { get; private set; }


        private AccountUser() { }

        public AccountUser(string username)
        {
            Id = Guid.NewGuid();
            AccountId = Guid.NewGuid();

            SetUsername(username);

            CreateDateTime = DateTimeOffset.UtcNow;
            UpdatedDateTime = DateTimeOffset.UtcNow;
        }

        public void SetUsername(string username)
        {
            Username = username;
            UsernameNormalized = username.ToUpperInvariant();
            UpdatedDateTime = DateTimeOffset.UtcNow;
        }
    }
}
