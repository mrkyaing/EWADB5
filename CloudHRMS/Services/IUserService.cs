using Microsoft.AspNetCore.Identity;

namespace CloudHRMS.Services
{
    public interface IUserService
    {
        Task<string> CreateUser(string userName, string email);
        Task<IdentityUser> FindByUserName(string userName);
    }
}
