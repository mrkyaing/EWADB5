namespace CloudHRMS.Services
{
    public interface IUserService
    {
        Task<string> CreateUser(string userName, string email);
    }
}
