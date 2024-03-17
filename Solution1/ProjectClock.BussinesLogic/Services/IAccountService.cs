
using ProjectClock.BusinessLogic.Dtos.AccountDtos;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IAccountService
    {
        Task<bool> DeleteAccount(DeleteAccountDto dto);
        Task<EditEmailResultDto> EditAccountEmail(EditEmailDto dto);
        Task<EditPasswordResultDto> EditAccountPassword(EditPasswordDto dto);
        Task<string> GetAccountEmail(int Id);
        Task<AccountDto> GetAccountDetails(int Id);
        Task<LoginResultDto> LoginAccount(LoginDto dto);
        Task<RegisterResultDto> RegisterAccount(RegisterDto dto);
    }
}