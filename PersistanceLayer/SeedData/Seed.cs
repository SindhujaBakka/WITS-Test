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
                new AccountUser(
                    name: "Alice Smith",
                    email: "alice@test.com",
                    password: "Password@123",
                    username: "Alice01"
                ),

                new AccountUser(
                    name: "Bob Johnson",
                    email: "bob@test.com",
                    password: "Password@123",
                    username: "BobUser99"
                ),

                new AccountUser(
                    name: "Charlie Brown",
                    email: "charlie@test.com",
                    password: "Password@123",
                    username: "Charlie7"
                )
            };

            await context.AccountUsers.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
