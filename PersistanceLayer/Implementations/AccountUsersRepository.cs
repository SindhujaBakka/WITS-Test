using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Persistance.Contracts;

namespace Persistance.Implementations
{
    public class AccountUsersRepository: IAccountUsersRepository
    {
        private readonly DataContext _context;

        public AccountUsersRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all user entries present in database
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountUser>> GetAccountUsersAsync(CancellationToken ct)
        {
            return await _context.AccountUsers.AsNoTracking().ToListAsync(ct);
        }

        /// <summary>
        /// Checks if the username already present in database or not.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HasUsernameExistAsync(string username, CancellationToken ct)
        {
            var takenByOther = await _context.AccountUsers
                .AsNoTracking()
                .AnyAsync(x => x.UsernameNormalized == username.ToUpperInvariant(), ct);

            return takenByOther;
        }

        /// <summary>
        /// Inserts ot Updates the USER entry.
        /// </summary>
        /// <returns></returns>
        public async Task<(bool Success, bool Created, string? Error, AccountUser? AccountUserResult)> UpsertAsync(string username, Guid? accountId, CancellationToken ct)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(ct);

            AccountUser? existing = null;
            bool created;

            if (accountId != null)
            {
                existing = await _context.AccountUsers.FirstOrDefaultAsync(x => x.AccountId == accountId, ct);

                if (existing == null)
                    return (false, false, "Account not found.", null);

                existing.SetUsername(username);
                created = false;

            }
            else
            {
                existing = new AccountUser(username);
                _context.AccountUsers.Add(existing);
                created = true;
            }

            try
            {
                await _context.SaveChangesAsync(ct);
                await tx.CommitAsync(ct);
                return (true, created, null, existing);
            }
            catch (DbUpdateException)
            {
                return (false, false, "Username already taken.", null);
            }
        }
    }
}
