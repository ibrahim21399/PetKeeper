using Domain.Dto.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Admin
{
    public interface IAdminGetUsers
    {
        Task<List<GetUserDto>> GetUsersbyStatus(bool status);
        Task<ServiceResponse<int>> DeleteUser(Guid userid);
    }
}
