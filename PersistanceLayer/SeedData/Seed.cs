using Models.Entities;

namespace Persistance.SeedData
{
    public class Seed
    {
        public static async Task SeedAccountUsers(DataContext context)
        {
            if (context.AccountUsers.Any())
                return;

            var users = new List<AccountUser>
            {
                new AccountUser(username: "Alice01" ),

                new AccountUser(username: "BobUser99"),

                new AccountUser(username: "Charlie7")
            };

            await context.AccountUsers.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
