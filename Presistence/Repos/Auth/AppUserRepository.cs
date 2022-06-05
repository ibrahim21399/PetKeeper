using Application.Interfaces.Repos.Auth;
using Domain.Entites.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repos.Auth
{
    public class AppUserRepository : IAppUserRepository
    {
        private UserManager<ApplicationUser> _userManager;

        public AppUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
    
        public async Task AddRoleToUser(ApplicationUser user, string Role)
        {
            await _userManager.AddToRoleAsync(user, Role);

        }

        public async Task<List<ApplicationUser>> GetAllBussinusOwner()
        {
            var user = await _userManager.Users.Where(a => a.BusinessName != "").Include(a => a.UserRoles).ToListAsync();
            return user;
        }

        public async Task<TokenEntity> GetToken(string userName, string password, string topSecretKey, string issuer, string audience)
        {
            try
            {
                var user = await _userManager.Users.Include(q => q.UserRoles).Where(q => q.UserName == userName).FirstOrDefaultAsync();
                if (user != null && await _userManager.CheckPasswordAsync(user, password))
                {
                    var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
                };


                    if (!user.IsActive)
                        return new TokenEntity
                        {
                            IsActive = false
                        };

                    var superSecretPassword = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(topSecretKey));

                    var token = new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        expires: DateTime.Now.AddDays(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(superSecretPassword, SecurityAlgorithms.HmacSha256)
                    );

                    return new TokenEntity
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        CurrentUser = user,
                        IsActive = user.IsActive
                    };
                }

                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var User = await _userManager.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return User;
        }
        public  ApplicationUser GetUserByEmail(string email)
        {
            var User =  _userManager.Users.Where(x => x.Email == email).FirstOrDefault();
            return User;
        }

        public async Task<ApplicationUser> GetUserById(Guid id)
        {
            var User =await _userManager.Users.Where(_x => _x.Id == id).FirstOrDefaultAsync();
            return User;
        }
    }
}
