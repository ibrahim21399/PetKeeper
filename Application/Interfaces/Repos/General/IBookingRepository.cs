using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos.General
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetAllBookingAsync(Guid BusId ,bool State,bool IsCancelled);
    }
}
