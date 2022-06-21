using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Services.Admin;
using Application.Interfaces.Services.BusinessOwner;
using Domain.Dto.Admin;
using Domain.Dto.Business;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin
{
    public class AcceptOrRefuseBusiness:ServiceBase,IAcceptOrRefuseBusiness
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessService _businessService;
        public AcceptOrRefuseBusiness(IBusinessRepository businessRepository,IUnitOfWork unitOfWork,IBusinessService businessService)
        {
            _businessRepository = businessRepository;
            _unitOfWork = unitOfWork;
            _businessService = businessService;
            
        }
        public async Task<ServiceResponse<int>> ApproveBusiness(Guid BusId)
        {
            try
            {
                var bus =_businessRepository.GetById(BusId);
                bus.IsActive = true;
                await _unitOfWork.CommitAsync();
                //string email = $"Dear Business Owner\n" +
                //    $",\ncongratulations Your Bussiness Was Approved, If you need any help contact us \n" +
                //    $"\n Regards,\n" +
                //    $"MyPet Team";
                //    MailMessage message = new MailMessage();
                //    SmtpClient smtp = new SmtpClient();

                //    message.From = new MailAddress("petkeeperteam@gmail.com");
                //    message.To.Add(new MailAddress("IbrahimMohamed21399@gmail.com"));
                //    message.Subject = "congratulations Your Bussiness Was Approved";
                //    message.IsBodyHtml = false; //to make message body as html  
                //    message.Body = email;
                //    smtp.Port = 587;
                //    smtp.Host = "smtp.gmail.com"; //for gmail host  
                //    smtp.EnableSsl = true;
                //    smtp.UseDefaultCredentials = false;
                //    smtp.Credentials = new NetworkCredential("petkeeperteam@gmail.com", "01023074032");
                //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    smtp.Send(message);
                return new ServiceResponse<int>
                {
                    Success = true,
                    Data = 1,
                    Message = "Approved Succesfully"
                };

            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }

        public async Task<ServiceResponse<List<GetBusinessDto>>> GetAllBusinuss(bool IsActive)
        {
            try
            {
               var bus=  _businessRepository.GetAll(a => a.IsActive == IsActive);
                var list = await _businessService.GetBusinessDtoList(bus);
                return new ServiceResponse<List<GetBusinessDto>>
                {
                    Data = list,
                    Success = true,
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ServiceResponse<GetAdminBusinessDetailsDto>> GetBussinesDetails(Guid BusId)
        {
            try
            {
                var bus = _businessRepository.GetById(BusId);
                var map =await _businessService.GetBusinessDetailesDtoList(bus);
                return new ServiceResponse<GetAdminBusinessDetailsDto>
                {
                    Data = map,
                    Success = true,
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Task<ServiceResponse<int>> UnApproveBusiness(Guid BusId)
        {
            throw new NotImplementedException();
        }
    }
}
