using Models.Entities;

namespace Persistance.SeedData
{
    public class Seed
    {
        /// <summary>
        /// Seed Data to be inserted for the first time when the database gets created.
        /// </summary>
        /// <returns></returns>
        public static async Task SeedAccountUsers(DataContext context)
        {
            if (context.AccountUsers.Any())
                return;

            var users = new List<AccountUser>
            {
                new AccountUser(username: "Sindhuja Bakkashetti" ),

                new AccountUser(username: "Sindhu9040f"),

                new AccountUser(username: "SeedUser123")
            };

            await context.AccountUsers.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
