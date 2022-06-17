using Application.Interfaces.Repos.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repos.General
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Booking>> GetAllBookingAsync(Guid BusId, bool State,bool IsCancelled)
        {
            var bookings =  _dbContext.Bookings.Where(a => a.BusinessId == BusId && a.status == State && a.IsCanceled ==IsCancelled).ToList();
            return bookings;
        }
    }
}
