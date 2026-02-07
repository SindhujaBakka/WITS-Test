using AutoMapper;
using Models.Dtos;
using Persistance.Contracts;
using Services.Contracts;
using Services.Helpers;

namespace Services.Implementations
{
    public class AccountUsersService : IAccountUsersService
    {
        private readonly IAccountUsersRepository _accountUsersRepo;
        private readonly IMapper _mapper;

        public AccountUsersService(IAccountUsersRepository accountUsersRepository, IMapper mapper)
        {
            _accountUsersRepo = accountUsersRepository;
            _mapper = mapper;
        }

        public async Task<(bool IsValidFormat, bool IsAvailable, List<string> Errors)> ValidateAccountUser(string username, CancellationToken ct)
        {
            var (isValid, errors) = UsernameRules.Validate(username);
            if (!isValid)
                return (false, false, errors);

            var exists = await _accountUsersRepo.HasUsernameExistAsync(username!, ct);

            return (true, !exists, errors);
        }

        public async Task<(bool Success, bool Created, string? Error, AccountUserDto? Result)> UpsertAccountUser(string username, Guid? accountId, CancellationToken ct)
        {
            var (isValid, errors) = UsernameRules.Validate(username);
            if (!isValid)
                return (false, false, string.Join(" ", errors), null);

            var result = await _accountUsersRepo.UpsertAsync(username, accountId, ct);

            return (result.Success, result.Created, result.Error, _mapper.Map<AccountUserDto>(result.AccountUserResult));
        }
    }
}
