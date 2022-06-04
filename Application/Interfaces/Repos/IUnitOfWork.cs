using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Interfaces.Repos
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

    }
}
