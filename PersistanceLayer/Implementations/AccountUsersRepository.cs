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

        public async Task<bool> HasUsernameExistAsync(string username, CancellationToken ct)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(ct);

            var takenByOther = await _context.AccountUsers
                .AsNoTracking()
                .AnyAsync(x => x.UsernameNormalized == username.ToUpperInvariant(), ct);

            return takenByOther;
        }

        public async Task<(bool Success, bool Created, string? Error, AccountUser? AccountUserResult)> UpsertAsync(Guid accountId, string? username, CancellationToken ct)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(ct);

            var existing = await _context.AccountUsers.SingleOrDefaultAsync(x => x.AccountId == accountId, ct);

            bool created;
            if (existing is null)
            {
                _context.AccountUsers.Add(new AccountUser(username!));
                created = true;
            }
            else
            {
                existing.SetUsername(username!);
                created = false;
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
