using Domain.Dto.General.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Auth
{
    public interface IUserService
    {
        Task<ServiceResponse<TokenDto>> Token(LoginDto loginDto);
        Task<ServiceResponse<int>> RegisterAccounUser(RegisterDto registerAccountUserDto,bool status);
        Task<ServiceResponse<int>> SigOutAsync();
        Task<ServiceResponse<int>> UpdateUser(Guid id, UserDto userDto);
        Task DeletAccountUser(Guid Id);
        Task<ServiceResponse<int>> ChangePassword(Guid Id, string CurrentPass, String NewPass);
        
    }
}
