using Application.Interfaces.Repositories.General;
using Application.Interfaces.Services.General;
using AutoMapper;
using Domain.Dto.General;
using Domain.Entites.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.General
{
    public class FileService : IFileService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private IHostingEnvironment Environment;
        private readonly IUnitOfWork _unitOfWork;

        public FileService(
            IAttachmentRepository attachmentRepository, IMapper mapper, IHostingEnvironment _Environment,IUnitOfWork unitOfWork)
        {
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
            Environment = _Environment;
            _unitOfWork = unitOfWork;

        }
        public async Task<KeyValuePair<bool, string>> UploadFile(Guid rowId, Guid? scondRowId, List<IFormFile> UploadFiles, string TableName, string fileTypeCode, string FolderName, int fileMaxSize = 200000)
        {
            string message = "";
            if (UploadFiles.Count == 0)
                return new KeyValuePair<bool, string>(false, "picturehasnotbeenuploaded");

            try
            {
                for (int i = 0; i < UploadFiles.Count; i++)
                {
                    #region set attachment model
                    var model = new AttachmentDto()
                    {
                        RowId = rowId.ToString(),
                        Id = Guid.NewGuid(),
                        //SecondRowId = scondRowId,
                        FileName = UploadFiles[i].FileName,
                        FileExtension = Path.GetExtension(UploadFiles[i].FileName),
                        FileLength = UploadFiles[i].Length,
                        MIMEType = UploadFiles[i].ContentType,
                        TableName = TableName
                    };

                    string path = Path.Combine(this.Environment.WebRootPath, FolderName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    model.FilePath = path;
                    #endregion
                    if (!Directory.Exists(Path.GetDirectoryName(model.FilePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(model.FilePath));
                    string fileName = Path.GetFileName(UploadFiles[i].FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        UploadFiles[i].CopyTo(stream);

                    }
                    var attachment = _mapper.Map<Attachment>(model);
                    attachment.File_Type_Code = "000";
                    //Domain.Enums.MemiTypes.GetTypeByMemi(model.MIMEType).ToString();
                    attachment.File_Path = FolderName + "/" + UploadFiles[i].FileName;
                   _attachmentRepository.Create(attachment);
                    
                }
                return new KeyValuePair<bool, string>(true, "Success");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }
        private bool CheckFile(IFormFile uploadedFile, out string message, int fileMaxSize)
        {
            if (uploadedFile.Length > fileMaxSize)
            {
                message = "UploadFileSizeValidMSG";
                return false;
            }
            message = "";
            return true;
        }

        public bool DeleteFilePhysical(string path)
        {
            string fullPath = Path.Combine(this.Environment.WebRootPath, path);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }
}

