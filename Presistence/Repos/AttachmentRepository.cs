using Application.Interfaces.Repositories.General;
using Domain.Entites.General;
using Microsoft.AspNetCore.Hosting;
using Presistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.General
{
    public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        private IHostingEnvironment Environment;
        public AttachmentRepository(ApplicationDbContext dbContext, IHostingEnvironment environment) : base(dbContext)
        {
            Environment = environment;
        }

        public void PhysiscalDelete(Guid rowid)
        {
            var attachment = _dbContext.Attachments.Where(a => a.Row_Id == rowid.ToString()).FirstOrDefault();
            if (attachment is not null)
            {
                string fullPath = Path.Combine(this.Environment.WebRootPath, attachment.File_Path);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                _dbContext.Attachments.Remove(attachment);
            }
        }

        public Attachment GetAttach(Guid rowid,string table)
        {
            var attachment = _dbContext.Attachments.Where(a => a.Row_Id == rowid.ToString()&&a.Table_Name==table).FirstOrDefault();
            if (attachment is not null) return attachment;

            else return null;


        }
    }
}
