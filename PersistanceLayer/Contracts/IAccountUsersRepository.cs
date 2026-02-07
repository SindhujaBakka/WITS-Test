using Models.Entities;

namespace Persistance.Contracts
{
    public interface IAccountUsersRepository
    {
        Task<List<AccountUser>> GetAccountUsersAsync(CancellationToken ct);
        Task<bool> HasUsernameExistAsync(string username, CancellationToken ct);
        Task<(bool Success, bool Created, string? Error, AccountUser? AccountUserResult)> UpsertAsync(string username, Guid? accountId, CancellationToken ct);
    }
}
