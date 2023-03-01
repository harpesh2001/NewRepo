using CI_Platform_three_tier.DataModels.DataModels;
using CI_Platform_three_tier.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace CI_Platform_three_tier.Repository.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly PlatformDbContext _dbContext;

        public UserRepository()
        {
            _dbContext = new PlatformDbContext();
        }

        public async Task<int> RegisterUserAsync(User user)
        {
            var userDetails = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,

            };

            _dbContext.Users.Add(userDetails);
            await _dbContext.SaveChangesAsync();
            return 1;

        }

        async Task<int> IUserRepository.AddToken(PasswordReset model, string token)
        {
            try
            {
                var tokendetails = new PasswordReset()
                {
                    Email = model.Email,
                    Token = token
                };
                _dbContext.PasswordResets.Add(tokendetails);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        int IUserRepository.CheckUserAsync(User user)
        {
            var UsersList = _dbContext.Users.Where(X => X.Email == user.Email ).FirstOrDefault();
            if (UsersList == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        int IUserRepository.CheckUserAsync(string email)
        {
            var UsersList = _dbContext.Users.Where(X => X.Email == email).FirstOrDefault();
            if (UsersList == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        async Task<List<User>> IUserRepository.GetAllUsersAsync()
        {
            return await _dbContext.Users.Select(
                User => new User()
                {
                    UserId = User.UserId,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    PhoneNumber = User.PhoneNumber,
                    Email = User.Email,

                }).ToListAsync();
        }


        int IUserRepository.VerifyUserAsync(User user)
        {


            var UsersList = _dbContext.Users.Where(X => X.Email == user.Email && X.Password == user.Password).FirstOrDefault();
            if (UsersList == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
