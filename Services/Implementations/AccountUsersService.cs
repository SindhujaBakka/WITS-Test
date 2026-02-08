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

        /// <summary>
        /// Gets all the User entries present in database
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountUserDto>> GetAccountUsers(CancellationToken ct)
        {
            var users = await _accountUsersRepo.GetAccountUsersAsync(ct);
            return _mapper.Map<List<AccountUserDto>>(users);
        }

        /// <summary>
        /// Method to validate the User Name
        /// </summary>
        /// <param name="username">Username to be validated</param>
        /// <returns></returns>
        public async Task<(bool IsValidFormat, bool IsAvailable, List<string> Errors)> ValidateAccountUser(string username, CancellationToken ct)
        {
            var (isValid, errors) = UsernameRules.Validate(username);
            if (!isValid)
                return (false, false, errors);

            var exists = await _accountUsersRepo.HasUsernameExistAsync(username!, ct);

            return (true, !exists, errors);
        }

        /// <summary>
        /// Inserts or Updates the User Record
        /// </summary>
        /// <param name="username">User Name to be inserted</param>
        /// <param name="accountId">Account ID</param>
        /// <returns></returns>
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
