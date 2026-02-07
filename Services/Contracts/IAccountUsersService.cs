using Models.Dtos;

namespace Services.Contracts
{
    public interface IAccountUsersService
    {
        Task<(bool IsValidFormat, bool IsAvailable, List<string> Errors)> ValidateAccountUser(string username, CancellationToken ct);

        Task<(bool Success, bool Created, string? Error, AccountUserDto? Result)> UpsertAccountUser(string username, Guid? accountId, CancellationToken ct);
    }
}
