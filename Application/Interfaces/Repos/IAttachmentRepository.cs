using Application.Interfaces.Repos;
using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.General
{
    public interface IAttachmentRepository:IBaseRepository<Attachment>
    {
        void PhysiscalDelete(Guid rowId);
        Attachment GetAttach(Guid rowid, string table);

    }
}
