namespace E_commerceManagementSystem.BLL.Manager.OtpManager
{
    public interface IOtpManager
    {
        Task<string> GenerateOtpAsync(string email);
        Task RemoveOtpAsync(string email);
    }
}
