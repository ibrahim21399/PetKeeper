using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos.Auth
{
    public interface IAppUserRepository
    {
        Task<TokenEntity> GetToken(string userName, string password, string topSecretKey, string issuer, string audience);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        ApplicationUser GetUserByEmail(string email);
        Task<ApplicationUser> GetUserById(Guid id);
        Task AddRoleToUser(ApplicationUser user, string Role);
        //Task<List<ApplicationUser>> GetEmployers();
    }
}
