using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class AccountUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid AccountId { get; private set; }
        public string Username { get; private set; } = default!;
        public string UsernameNormalized { get; private set; } = default!;

        public DateTimeOffset CreateDateTime { get; private set; }
        public DateTimeOffset UpdatedDateTime { get; private set; }
    }
}
