using Models.Entities;

namespace Persistance.Contracts
{
    public interface IAccountUsersRepository
    {
        Task<bool> HasUsernameExistAsync(string username, CancellationToken ct);
        Task<(bool Success, bool Created, string? Error, AccountUser? AccountUserResult)> UpsertAsync(Guid accountId, string? username, CancellationToken ct);
    }
}
