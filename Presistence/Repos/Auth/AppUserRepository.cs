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

        //public async Task<List<ApplicationUser>> GetAllBussinusOwner()
        //{
        //    var user = await _userManager.Users.Where(a => a != "").Include(a => a.UserRoles).ToListAsync();
        //    return user;
        //}

        public async Task<TokenEntity> GetToken(string userName, string password, string topSecretKey, string issuer, string audience)
        {
            try
            {
                var user =  _userManager.Users.Where(q => q.UserName == userName).FirstOrDefault();
                var Role = await _userManager.GetRolesAsync(user);
                var userclaim = await _userManager.GetClaimsAsync(user);
                var roleClamis =new List<Claim>();  
                foreach (var role in Role)
                {
                    roleClamis.Add(new Claim("Role",role));

                }
                if (user != null && await _userManager.CheckPasswordAsync(user, password))
                {
                    var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, Role[0])

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

        public string GetUserFullName(Guid id)
        {
            var user =  _userManager.Users.Where(x => x.Id == id).Select(x => x.FullName).FirstOrDefault();
            return user;
        }
    }
}
