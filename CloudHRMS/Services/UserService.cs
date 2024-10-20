using Microsoft.AspNetCore.Identity;


namespace CloudHRMS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> CreateUser(string userName, string email)
        {
            var user = CreateUser();
            user.Email = email;//pass the email of employee data
            user.UserName = email;//pass the email of employee data
            user.NormalizedUserName = userName;
            user.NormalizedEmail = email;

            var result = await _userManager.CreateAsync(user, "I10V@akog02MG");//create a user with default password
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");//assign created user to "Employee" role
                return user.Id;
            }
            else
            {
                return string.Empty;
            }
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
