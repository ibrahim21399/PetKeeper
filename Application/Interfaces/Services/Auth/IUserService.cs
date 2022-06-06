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
    }
}
