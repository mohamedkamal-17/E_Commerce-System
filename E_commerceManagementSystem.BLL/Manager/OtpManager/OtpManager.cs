using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace E_commerceManagementSystem.BLL.Manager.OtpManager
{
    public class OtpManager : IOtpManager
    {

        private readonly IMemoryCache _cache;

        public OtpManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task<string> GenerateOtpAsync(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            _cache.Set($"{email}_Verified", otp, TimeSpan.FromMinutes(10));
            return otp;
        }
        public async Task RemoveOtpAsync(string email)
        {
            _cache.Remove($"{email}_Verified");
        }
    }
}