using System.ComponentModel.DataAnnotations;
namespace Models.Entities
{
    public class AccountUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Guid AccountId { get; private set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Username { get; set; }

        public string UsernameNormalized { get; private set; }

        public DateTimeOffset CreateDateTime { get; private set; }

        public DateTimeOffset UpdatedDateTime { get; private set; }
    }
}
