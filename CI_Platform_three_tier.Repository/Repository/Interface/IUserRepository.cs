using CI_Platform_three_tier.DataModels.DataModels;

namespace CI_Platform_three_tier.Repository.Repository.Interface
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        int VerifyUserAsync(User user);
        int CheckUserAsync(User user);
        int CheckUserAsync(string email);
        Task<int> AddToken(PasswordReset model, string token);
    }
}