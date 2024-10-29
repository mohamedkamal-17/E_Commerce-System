using E_commerceManagementSystem.BLL.Dtos.OtpDto;
using E_commerceManagementSystem.BLL.Dtos.OtpDto.OtpDto;
using E_commerceManagementSystem.BLL.DTOs.AccountDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;

namespace E_commerceManagementSystem.BLL.Manager.AccountManager
{
    public interface IAccountManager
    {
        GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)


; Task<GeneralRespons> RegisterAsync(UserRegisterDTO registerVM);
        Task<GeneralAccountResponse> LoginAsync(UserLoginDTO UserLoginDTO);
        Task LogOutAsync();

        Task<GeneralAccountResponse> SendOtpForPasswordReset(SendOtpRequestDto dto);
        Task<GeneralAccountResponse> VerifyOtp(VerifyOtpRequestDto dto);
        Task<GeneralAccountResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto);
    }

}
